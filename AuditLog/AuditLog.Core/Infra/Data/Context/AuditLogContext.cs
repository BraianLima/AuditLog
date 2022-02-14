using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Infra.Data.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.Core.Infra.Data.Context
{
    public class AuditLogContext : BaseContext
    {
        public AuditLogContext(DbContextOptions<AuditLogContext> options, IHttpContextAccessor httpContextAccessor, IMapper mapper) 
            : base(options, httpContextAccessor, mapper)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new AuditMap());
        }


    }
}
