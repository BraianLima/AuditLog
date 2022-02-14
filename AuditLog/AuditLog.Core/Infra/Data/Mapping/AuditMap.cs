using AuditLog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditLog.Core.Infra.Data.Mapping
{
    public class AuditMap : IEntityTypeConfiguration<Audit>
    {
        void IEntityTypeConfiguration<Audit>.Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.Property(x => x.Id).HasColumnType("INT").IsRequired();
            builder.Property(x => x.Action).HasColumnType("VARCHAR(10)").IsRequired();
            builder.Property(x => x.TableName).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.OldObject).HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.NewObject).HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.CreatedDate).HasColumnType("DATETIME2");
            builder.Property(x => x.UserId).HasColumnType("INT");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder
                .HasOne(x => x.User)
                .WithMany(b => b.Audits)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_audits_users0_users");

            builder.ToTable("Audits");
        }
    }
}
