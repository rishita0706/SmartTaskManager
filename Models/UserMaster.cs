using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    [Table("UserMaster")]
    public class UserMaster
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        // Foreign keys
        public int RoleId { get; set; }

        public int DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(RoleId))]
        public RoleMaster? Role { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public DepartmentMaster? Department { get; set; }

        public ICollection<TaskMaster> AssignedTasks { get; set; } = new List<TaskMaster>();
        public ICollection<TaskMaster> CreatedTasks { get; set; } = new List<TaskMaster>();
        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
