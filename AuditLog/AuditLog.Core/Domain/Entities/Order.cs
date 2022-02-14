namespace AuditLog.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string ProductName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
