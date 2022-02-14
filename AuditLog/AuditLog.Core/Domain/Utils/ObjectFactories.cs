using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Enumerations;
using AuditLog.Core.Domain.Extensions;
using AuditLog.Core.Domain.Responses;
using System;
using System.Net;

namespace AuditLog.Core.Domain.Utils
{
    public class ObjectFactories
    {
        public Response ReturnResponseToService(bool success, bool isInsert, object data = null)
        {
            if (success)
            {
                if (isInsert)
                    return NewResponse(HttpStatusCode.Created, Messages.SUCCESS, data);

                return NewResponse(HttpStatusCode.OK, Messages.SUCCESS, data);
            }

            return NewResponse(HttpStatusCode.BadRequest, Messages.FAILURE);
        }

        private Response NewResponse(HttpStatusCode httpStatusCode, string message, object data = null)
        {
            return new Response
            {
                StatusCode = httpStatusCode,
                Data = data,
                Message = message
            };
        }

        public AuditDTO NewAuditDtoByLogin(User user)
        {
            return new AuditDTO
            {
                Action = LogEnum.LOGIN.GetDisplayName(),
                CreatedDate = DateTime.Now,
                NewObject = GenericMethods.SerializeObject(user),
                OldObject = null,
                UserId = user.Id,
                TableName = "User"
            };
        }
    }
}
