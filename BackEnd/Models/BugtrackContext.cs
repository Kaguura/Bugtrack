using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackEnd.Models
{
    public partial class BugtrackContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bugtrack;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.ProjectTask)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProjectTaskId)
                    .HasConstraintName("FK_dbo.Comments_dbo.ProjectTasks_ProjectTaskId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comments_AspNetUsers");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.FileName).HasMaxLength(256);

                entity.Property(e => e.Uploaded).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Files_ProjectTasks");
            });
            
            modelBuilder.Entity<ProjectRoles>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ProjectId });

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectRoles)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectRoles_Projects");

                /*
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ProjectRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectRoles_AspNetRoles");*/
            });

            modelBuilder.Entity<ProjectTaskHistory>(entity =>
            {
                entity.Property(e => e.AssignedUserId).HasMaxLength(450);

                entity.Property(e => e.ChangedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndedOn).HasColumnType("datetime");

                entity.Property(e => e.EstimatedEndsOn).HasColumnType("datetime");

                entity.Property(e => e.StartedOn).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTaskHistory)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectTaskHistory_Projects");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProjectTaskHistory)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_ProjectTaskHistory_Status");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.ProjectTaskHistory)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ProjectTaskHistory_ProjectTasks");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.ProjectTaskHistory)
                    .HasForeignKey(d => d.TaskTypeId)
                    .HasConstraintName("FK_ProjectTaskHistory_TaskTypes");
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectTaskHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ProjectTaskHistory_AspNetUsers");
            });

            modelBuilder.Entity<ProjectTasks>(entity =>
            {
                entity.Property(e => e.AssignedUserId).HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndedOn).HasColumnType("datetime");

                entity.Property(e => e.EstimatedEndsOn).HasColumnType("datetime");

                entity.Property(e => e.StartedOn).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.AssignedUser)
                    .WithMany(p => p.ProjectTasksAssignedUser)
                    .HasForeignKey(d => d.AssignedUserId)
                    .HasConstraintName("FK_ProjectTasks_AspNetUsers");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.InverseParentTask)
                    .HasForeignKey(d => d.ParentTaskId)
                    .HasConstraintName("FK_ProjectTasks_ProjectTasks");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectTasks_Projects");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_dbo.ProjectTasks_dbo.Status_StatusId");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.TaskTypeId)
                    .HasConstraintName("FK_dbo.ProjectTasks_dbo.TaskTypes_TaskTypeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectTasksUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ProjectTasks_AspNetUsers1");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Projects_Projects");
            });

            modelBuilder.Entity<UserBoardTasks>(entity =>
            {
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserBoardTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_UserBoardTasks_ProjectTasks");

                entity.HasOne(d => d.UserBoard)
                    .WithMany(p => p.UserBoardTasks)
                    .HasForeignKey(d => d.UserBoardId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserBoardTasks_UserBoards");
            });

            modelBuilder.Entity<UserBoards>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(450);
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBoards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserBoards_AspNetUsers");
            });
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<ProjectRoles> ProjectRoles { get; set; }
        public virtual DbSet<ProjectTaskHistory> ProjectTaskHistory { get; set; }
        public virtual DbSet<ProjectTasks> ProjectTasks { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TaskTypes> TaskTypes { get; set; }
        public virtual DbSet<UserBoardTasks> UserBoardTasks { get; set; }
        public virtual DbSet<UserBoards> UserBoards { get; set; }
    }
}