using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Models;

namespace SmartTaskManager.Data
{
    public class SmartTaskManagerDbContext : DbContext
    {
        public SmartTaskManagerDbContext(DbContextOptions<SmartTaskManagerDbContext> options)
           : base(options)
        {
        }

        //Master Tables
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<DepartmentMaster> DepartmentMasters { get; set; }
        public DbSet<PriorityMaster> PriorityMasters { get; set; }
        public DbSet<StatusMaster> StatusMasters { get; set; }

        // Transaction Tables
        public DbSet<UserMaster> UserMasters { get; set; }

        public DbSet<TaskMaster> TaskMasters { get; set; }

        public DbSet<TaskComment> TaskComments { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Role
            modelBuilder.Entity<UserMaster>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User -> Department
            modelBuilder.Entity<UserMaster>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // User -> Manager (self-referencing)
            modelBuilder.Entity<UserMaster>()
                .HasOne(u => u.Manager)
                .WithMany()
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task -> Priority
            modelBuilder.Entity<TaskMaster>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task -> Status
            modelBuilder.Entity<TaskMaster>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task -> AssignedTo (User)
            modelBuilder.Entity<TaskMaster>()
                .HasOne(t => t.AssignedEmployee)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Task -> AssignedBy (User)
            modelBuilder.Entity<TaskMaster>()
                .HasOne(t => t.AssignedManager)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Task -> Department
            modelBuilder.Entity<TaskMaster>()
                .HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment -> Task
            modelBuilder.Entity<TaskComment>()
                .HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment -> User
            modelBuilder.Entity<TaskComment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attachment -> Task
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Task)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attachment -> User
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.UploadedUser)
                .WithMany(u => u.Attachments)
                .HasForeignKey(a => a.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
