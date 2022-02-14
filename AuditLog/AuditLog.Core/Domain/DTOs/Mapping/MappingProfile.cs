using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.DTOs.Order;
using AuditLog.Core.Domain.DTOs.User;
using AutoMapper;

namespace AuditLog.Core.Domain.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.User, UserDTO>();
            CreateMap<Entities.User, UserViewDTO>();
            CreateMap<UserViewDTO, Entities.User>();
            CreateMap<UserDTO, Entities.User>();

            CreateMap<Entities.Order, OrderDTO>();
            CreateMap<OrderDTO, Entities.Order>();

            CreateMap<Entities.Audit, AuditDTO>();
            CreateMap<AuditDTO, Entities.Audit>();
            CreateMap<Entities.Audit, AuditViewDTO>();
            CreateMap<AuditViewDTO, Entities.Audit>();
            CreateMap<AuditViewDTO, AuditDTO>();
            CreateMap<AuditDTO, AuditViewDTO>();
        }
    }
}
