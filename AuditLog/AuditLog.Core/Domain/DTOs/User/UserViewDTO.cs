using System;
using System.ComponentModel.DataAnnotations;

namespace AuditLog.Core.Domain.DTOs.User
{
    public class UserViewDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        public DateTime CreatedDate { get; set; }
    }
}
