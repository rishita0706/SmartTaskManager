using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        public int TaskId { get; set; }

        [StringLength(255)]
        public string? FileName { get; set; }

        [StringLength(500)]
        public string? FilePath { get; set; }

        public int UploadedBy { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TaskId))]
        public TaskMaster? Task { get; set; }

        [ForeignKey(nameof(UploadedBy))]
        public UserMaster? UploadedUser { get; set; }
    }
}