using AuditLog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditLog.Core.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).HasColumnType("int").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("varchar(2000)").IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.ToTable("Users");
        }
    }
}
