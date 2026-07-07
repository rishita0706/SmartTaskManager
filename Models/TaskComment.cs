using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    public class TaskComment
    {
        [Key]
        public int CommentId { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public string? Comment { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TaskId))]
        public TaskMaster? Task { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserMaster? User { get; set; }
    }
}