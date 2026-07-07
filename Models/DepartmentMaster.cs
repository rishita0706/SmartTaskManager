using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.Models
{
    public class DepartmentMaster
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<UserMaster> Users { get; set; } = new List<UserMaster>();
    }
}