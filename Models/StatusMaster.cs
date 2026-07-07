using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.Models
{
    public class StatusMaster
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public ICollection<TaskMaster> Tasks { get; set; } = new List<TaskMaster>();
    }
}