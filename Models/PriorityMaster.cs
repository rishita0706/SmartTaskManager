using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    [Table("PriorityMaster")]
    public class PriorityMaster
    {
        [Key]
        public int PriorityId { get; set; }

        [Required]
        [StringLength(30)]
        public string PriorityName { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public ICollection<TaskMaster> Tasks { get; set; } = new List<TaskMaster>();
    }
}