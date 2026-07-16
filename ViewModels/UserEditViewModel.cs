using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.ViewModels
{
    public class UserEditViewModel
    {
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Optional on edit — blank to keep existing password.
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? ManagerId { get; set; }

        public bool IsActive { get; set; }
    }
}