using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Global;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Handlers.Validators;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Domain.Utils;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Core.Domain.Handlers
{
    public class AuditHandler : BaseHandlers, IAuditHandler
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;
        private readonly ObjectFactories _objectFactories;
        private readonly AuditValidator _auditValidator;

        public AuditHandler(IAuditRepository auditRepository,
            IMapper mapper,
            ObjectFactories objectFactories,
            AuditValidator auditValidator)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
            _objectFactories = objectFactories;
            _auditValidator = auditValidator;
        }

        public async Task<Response> GetListAuditsByUserIdAsync(int userId)
        {
            var listAudits = await _auditRepository.GetListAuditsByUserIdAsync(userId);
            var isValid = ExecuteValidate(() => _auditValidator.ListAuditsIsValid(listAudits));

            if (isValid)
            {
                var listAuditDtos = SetUserNameInAuditDTO(listAudits, ListEntitiesToListAuditDtos(listAudits));
                
                return _objectFactories
                    .ReturnResponseToService(
                        isValid, 
                        GlobalVariables.INSERT_FALSE, ListAuditDtosToListAuditViewDtos(listAuditDtos));
            }

            return _objectFactories
                .ReturnResponseToService(isValid, GlobalVariables.INSERT_FALSE, null);
        }

        public async Task<Response> InsertAsync(AuditDTO auditDto)
        {
            var isValid = ExecuteValidate(() => _auditValidator.AuditDtoIsValid(auditDto));
            if (isValid)
            {
                var audit = await _auditRepository.InsertAsync(AuditDtoToEntity(auditDto));
                var insertIsValid = ExecuteValidate(() => _auditValidator.InsertIsValid(audit));

                return _objectFactories.ReturnResponseToService(insertIsValid, GlobalVariables.IS_INSERT, EntityToAuditDto(audit));
            }

            return (Response)await TaskRunAsync(() =>
                _objectFactories.ReturnResponseToService(isValid, GlobalVariables.IS_INSERT, auditDto));
        }

        private static List<AuditDTO> SetUserNameInAuditDTO(List<Audit> listAudits, List<AuditDTO> listAuditDtos)
        {
            listAuditDtos.ForEach(audit =>
                audit.UserName = listAudits
                    .Where(x => x.UserId == audit.UserId)
                    .Select(x => x.User.Name)
                    .FirstOrDefault()
            );

            return listAuditDtos;
        }

        #region [ Map Audit ]

        private AuditDTO EntityToAuditDto(Audit audit)
        {
            return _mapper.Map<AuditDTO>(audit);
        }

        private Audit AuditDtoToEntity(AuditDTO auditDto)
        {
            return _mapper.Map<Audit>(auditDto);
        }

        private List<AuditDTO> ListEntitiesToListAuditDtos(List<Audit> listAudits)
        {
            return _mapper.Map<List<AuditDTO>>(listAudits);
        }

        private List<AuditViewDTO> ListAuditDtosToListAuditViewDtos(List<AuditDTO> listAuditDtos)
        {
            return _mapper.Map<List<AuditViewDTO>>(listAuditDtos);
        }

        #endregion
    }
}
