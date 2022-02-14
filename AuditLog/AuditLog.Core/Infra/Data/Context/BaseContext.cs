using AuditLog.Core.Domain.DTOs.Audit;
using AuditLog.Core.Domain.Entities;
using AuditLog.Core.Domain.Enumerations;
using AuditLog.Core.Domain.Extensions;
using AuditLog.Core.Domain.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuditLog.Core.Infra.Data.Context
{
    public abstract class BaseContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public BaseContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<int> SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();

            if (!ChangeTracker.Entries().Any())
                return 0;

            var listAudits = new List<Audit>();
            var listEntityEntries = GetEntitiesEntries();

            foreach (var entry in listEntityEntries)
            {
                var auditDto = GetAuditDTO(entry);

                if (SaveAudit(auditDto))
                    listAudits.Add(AuditDtoToEntity(auditDto));
            }

            if (listAudits.Any())
                await AddRangeAsync(listAudits);

            return await base.SaveChangesAsync();
        }

        private List<EntityEntry> GetEntitiesEntries()
        {
            return ChangeTracker.Entries().Where(x => (x.State == EntityState.Added
                || x.State == EntityState.Modified || x.State == EntityState.Deleted) &&
                x.Entity is not Audit).ToList();
        }

        private AuditDTO GetAuditDTO(EntityEntry entry)
        {
            AuditDTO auditDto = new();

            if (entry.State == EntityState.Added)
            {
                auditDto = GetOldAndNewObject(entry, LogEnum.INSERT);
                auditDto.Action = LogEnum.INSERT.GetDisplayName();
                auditDto.DifferentObjects = true;
            }

            if (entry.State == EntityState.Modified)
            {
                auditDto = GetOldAndNewObject(entry, LogEnum.UPDATE);
                auditDto.Action = LogEnum.UPDATE.GetDisplayName();
                auditDto.DifferentObjects = GenericMethods
                    .DifferentObjects(auditDto.OldObjectTypeObject, auditDto.NewObjectTypeObject);
            }

            if (entry.State == EntityState.Deleted)
            {
                auditDto = GetOldAndNewObject(entry, LogEnum.DELETE);
                auditDto.Action = LogEnum.DELETE.GetDisplayName();
                auditDto.DifferentObjects = true;
            }

            auditDto.TableName = GetTableName(entry);
            auditDto.UserId = GetUserId();
            auditDto.CreatedDate = DateTime.Now;
            auditDto.NewObject = auditDto.NewObjectTypeObject != null ? GenericMethods.SerializeObject(auditDto.NewObjectTypeObject) : null;
            auditDto.OldObject = auditDto.OldObjectTypeObject != null ? GenericMethods.SerializeObject(auditDto.OldObjectTypeObject) : null;

            return auditDto;
        }

        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
        }

        private static string GetTableName(EntityEntry entityEntry)
        {
            return entityEntry.Entity.GetType().Name;
        }

        private static AuditDTO GetOldAndNewObject(EntityEntry entity, LogEnum logAction)
        {
            if (LogEnum.UPDATE == logAction)
            {
                return new AuditDTO
                {
                    OldObjectTypeObject = entity.OriginalValues.ToObject(),
                    NewObjectTypeObject = entity.CurrentValues.ToObject()
                };
            }

            if (LogEnum.INSERT == logAction)
            {
                return new AuditDTO
                {
                    OldObjectTypeObject = null,
                    NewObjectTypeObject = entity.CurrentValues.ToObject()
                };
            }

            return new AuditDTO
            {
                OldObjectTypeObject = entity.OriginalValues.ToObject(),
                NewObjectTypeObject = null
            };
        }

        private Audit AuditDtoToEntity(AuditDTO auditDto)
        {
            return _mapper.Map<Audit>(auditDto);
        }

        private static bool SaveAudit(AuditDTO auditDTO)
        {
            return auditDTO.DifferentObjects;
        }
    }
}
