using System.ComponentModel.DataAnnotations;

namespace AuditLog.Core.Domain.DTOs.Order
{
    public class OrderDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(20, ErrorMessage = "Product name cannot be longer than 20 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }
    }
}
