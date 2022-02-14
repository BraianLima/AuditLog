using AuditLog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditLog.Core.Infra.Data.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Id).HasColumnType("int").IsRequired();
            builder.Property(x => x.ProductName).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.UserId).HasColumnType("int");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder
                .HasOne(x => x.User)
                .WithMany(b => b.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_order_user0_user");

            builder.ToTable("Orders");
        }

    }
}
