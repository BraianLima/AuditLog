using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.DTOs.User;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Global;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Handlers.Validators;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Domain.Utils;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using AuditLog.Core.Services.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers
{
    public class UserHandler : BaseHandlers, IUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserValidator _userValidator;
        private readonly ObjectFactories _objectFactories;
        private readonly IAuditService _auditService;

        public UserHandler(IUserRepository userRepository,
            IMapper mapper,
            UserValidator userValidator,
            ObjectFactories objectFactories,
            IAuditService auditService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userValidator = userValidator;
            _objectFactories = objectFactories;
            _auditService = auditService;
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var isValid = ExecuteValidate(() => _userValidator.UserIsValid(user));

            return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, EntityToUserViewDto(user));
        }

        public async Task<Response> InsertAsync(UserDTO userDto)
        {
            var isValid = ExecuteValidate(() => _userValidator.UserDtoIsValid(userDto));
            if (isValid)
            {
                var user = await _userRepository.InsertAsync(PrepareUserDtoToUserToDatabase(userDto, true));
                var insertIsValid = ExecuteValidate(() => _userValidator.InsertIsValid(user));

                return _objectFactories.ReturnResponseToService(insertIsValid, GlobalVariables.IS_INSERT, EntityToUserViewDto(user));
            }

            return (Response)await TaskRunAsync(() =>
                _objectFactories.ReturnResponseToService(isValid, GlobalVariables.IS_INSERT, userDto));
        }

        public async Task<Response> UpdateAsync(UserDTO userDto)
        {
            var containId = _userValidator.ContainId(userDto.Id);

            if (containId)
            {
                var user = await _userRepository.GetByIdAsync(userDto.Id.Value);
                var isValid = _userValidator.UserIsValid(user);

                if (isValid)
                {
                    var updateValid  = await _userRepository.UpdateAsync(
                        PrepareUserDtoToUserToDatabase(userDto, false, user.CreatedDate));
                    return _objectFactories.ReturnResponseToService(updateValid, GlobalVariables.INSERT_FALSE, userDto);
                }

                return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, userDto);
            }

            return _objectFactories.ReturnResponseToService(containId, GlobalVariables.INSERT_FALSE, userDto);

        }

        public async Task<Response> DeleteByIdAsync(int id)
        {
            var isValid = await _userRepository.DeleteByIdAsync(id);
            return _objectFactories.ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, id);
        }

        public async Task<Response> LoginAsync(UserAuthenticationDTO userAuthenticationDto)
        {
            var response = await CheckUserAsync(userAuthenticationDto);
            var isValid = _userValidator.LoginIsValid(response);

            if (isValid)
            {
                var user = (User)response.Data;
                await SaveAuditAsync(_objectFactories.NewAuditDtoByLogin(user));
                return await GenerateToken(EntityToUserDto(user));
            }

            return response;
        }

        private async Task<Response> CheckUserAsync(UserAuthenticationDTO userAuthenticationDto)
        {
            var user = await _userRepository.GetByNameAsync(userAuthenticationDto.Name);
            var isValid = ExecuteValidate(() => _userValidator.UserIsValid(user));

            if (isValid)
            {
                var passwordIsValid = ExecuteValidate(() => 
                    _userValidator.PasswordIsValid(userAuthenticationDto.Password, user.Password));

                if (passwordIsValid)
                {
                    return _objectFactories
                        .ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, user);
                }
                return _objectFactories
                    .ReturnResponseToService(passwordIsValid, GlobalVariables.INSERT_FALSE, null);
            }

            return _objectFactories
                .ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, null);
        }

        private async Task<Response> GenerateToken(UserDTO userDto)
        {
            return (Response)await TaskRunAsync(() =>
            {
                return _objectFactories
                .ReturnResponseToService(
                    true,
                    GlobalVariables.INSERT_FALSE,
                    TokenJwt.GenerateToken(UserDtoToEntity(userDto)));
            });
        }

        private User PrepareUserDtoToUserToDatabase(UserDTO userDto, bool fromInsert, DateTime? createdDate = null)
        {
            var user = UserDtoToEntity(userDto);

            user.Password = EncryptPassword(user.Password);
            if (fromInsert)
            {
                user.CreatedDate = DateTime.Now;
                return user;
            }

            user.CreatedDate = createdDate.Value;
            return user;
        }

        private static string EncryptPassword(string password)
        {
            return HashCryptography.Encrypt(password);
        }

        private async Task SaveAuditAsync(AuditDTO auditDto)
        {
            await _auditService.InsertAsync(auditDto);
        }

        #region [ Map User ]

        private User UserDtoToEntity(UserDTO userDto)
        {
            return _mapper.Map<User>(userDto);
        }

        private UserDTO EntityToUserDto(User user)
        {
            return _mapper.Map<UserDTO>(user);
        }

        private UserViewDTO EntityToUserViewDto(User user)
        {
            return _mapper.Map<UserViewDTO>(user);
        }

        #endregion

    }
}
