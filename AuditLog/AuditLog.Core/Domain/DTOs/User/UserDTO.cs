using System.ComponentModel.DataAnnotations;

namespace AuditLog.Core.Domain.DTOs.User
{
    public class UserDTO 
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
