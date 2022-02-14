using AuditLog.Core.Domain.DTOs;
using AuditLog.Core.Domain.DTOs.User;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Responses;
using AuditLog.Core.Domain.Utils;
using System.Net;

namespace AuditLog.Core.Domain.Handlers.Validators
{
    public class UserValidator : BaseValidator
    {
        public bool UserIsValid(User user)
        {
            if (GenericMethods.IsNull(user))
                return false;

            if (GenericMethods.IsNullOrEmpty(user.Name) || GenericMethods.IsNullOrEmpty(user.Password))
                return false;

            return true;
        }

        public bool UserDtoIsValid(UserDTO userDto)
        {
            if (GenericMethods.IsNull(userDto))
                return false;

            if (GenericMethods.IsNullOrEmpty(userDto.Name) || GenericMethods.IsNullOrEmpty(userDto.Password))
                return false;

            return true;
        }

        public bool InsertIsValid(User user)
        {
            return GenericMethods.InsertIsValid(user);
        }

        public bool PasswordIsValid(string password, string passwordDataBase)
        {
            var passwordEncrypt = HashCryptography.Encrypt(password);
            
            if (passwordEncrypt == passwordDataBase)
                return  true;

            return false; ;
        }

        public bool LoginIsValid(Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK && typeof(User) == response.Data.GetType())
            {
                return true;
            }

            return false;
        }

    }
}
