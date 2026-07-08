using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    [Table("RoleMaster")]
    public class RoleMaster
    {   //Entity Framework maps this property to that SQL column
        [Key]
        public int RoleId { get; set; }   

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //used for navigation property, One role Many users that's why called a collection
        public ICollection<UserMaster> Users { get; set; } = new List<UserMaster>(); 
    }
}
