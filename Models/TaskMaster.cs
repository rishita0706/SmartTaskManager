using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    [Table("TaskMaster")]
    public class TaskMaster
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int PriorityId { get; set; }

        public int StatusId { get; set; }

        public int AssignedTo { get; set; }

        public int AssignedBy { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletedDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(PriorityId))]
        public PriorityMaster? Priority { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusMaster? Status { get; set; }

        [ForeignKey(nameof(AssignedTo))]
        public UserMaster? AssignedEmployee { get; set; }

        [ForeignKey(nameof(AssignedBy))]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public DepartmentMaster? Department { get; set; }
        public UserMaster? AssignedManager { get; set; }

        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();

        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}