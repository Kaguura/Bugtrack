using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BackEnd.Models;

namespace BackEnd.Migrations
{
    [DbContext(typeof(BugtrackContext))]
    [Migration("20170113051641_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Models.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ProjectTaskId");

                    b.Property<string>("Text");

                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("ProjectTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BackEnd.Models.Files", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("FileContent");

                    b.Property<string>("FileName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool?>("IsDeleted");

                    b.Property<int?>("TaskId");

                    b.Property<DateTime?>("Uploaded")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectRoles", b =>
                {
                    b.Property<string>("RoleId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<int?>("ProjectId");

                    b.Property<int>("Id");

                    b.HasKey("RoleId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RoleId");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("BackEnd.Models.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectTaskHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedUserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<DateTime>("ChangedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("CompletedPercent");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime?>("EndedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EstimatedEndsOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("ParentTaskId");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime?>("StartedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("StatusId");

                    b.Property<int>("TaskId");

                    b.Property<int?>("TaskTypeId");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TaskTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectTaskHistory");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedUserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<int?>("CompletedPercent");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime?>("EndedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EstimatedEndsOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("ParentTaskId");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("StatusId");

                    b.Property<int>("TaskTypeId");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Url");

                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ParentTaskId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TaskTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("BackEnd.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("BackEnd.Models.TaskTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TaskTypes");
                });

            modelBuilder.Entity("BackEnd.Models.UserBoards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsArchive");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserBoards");
                });

            modelBuilder.Entity("BackEnd.Models.UserBoardTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TaskId");

                    b.Property<int>("UserBoardId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserBoardId");

                    b.ToTable("UserBoardTasks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BackEnd.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser");


                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("BackEnd.Models.Comments", b =>
                {
                    b.HasOne("BackEnd.Models.ProjectTasks", "ProjectTask")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectTaskId")
                        .HasConstraintName("FK_dbo.Comments_dbo.ProjectTasks_ProjectTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackEnd.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Comments_AspNetUsers");
                });

            modelBuilder.Entity("BackEnd.Models.Files", b =>
                {
                    b.HasOne("BackEnd.Models.ProjectTasks", "Task")
                        .WithMany("Files")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK_Files_ProjectTasks");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectRoles", b =>
                {
                    b.HasOne("BackEnd.Models.Projects", "Project")
                        .WithMany("ProjectRoles")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_ProjectRoles_Projects")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BackEnd.Models.Projects", b =>
                {
                    b.HasOne("BackEnd.Models.Projects", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_Projects_Projects");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectTaskHistory", b =>
                {
                    b.HasOne("BackEnd.Models.Projects", "Project")
                        .WithMany("ProjectTaskHistory")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_ProjectTaskHistory_Projects");

                    b.HasOne("BackEnd.Models.Status", "Status")
                        .WithMany("ProjectTaskHistory")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_ProjectTaskHistory_Status");

                    b.HasOne("BackEnd.Models.ProjectTasks", "Task")
                        .WithMany("ProjectTaskHistory")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK_ProjectTaskHistory_ProjectTasks");

                    b.HasOne("BackEnd.Models.TaskTypes", "TaskType")
                        .WithMany("ProjectTaskHistory")
                        .HasForeignKey("TaskTypeId")
                        .HasConstraintName("FK_ProjectTaskHistory_TaskTypes");

                    b.HasOne("BackEnd.Models.User", "User")
                        .WithMany("ProjectTaskHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProjectTaskHistory_AspNetUsers");
                });

            modelBuilder.Entity("BackEnd.Models.ProjectTasks", b =>
                {
                    b.HasOne("BackEnd.Models.User", "AssignedUser")
                        .WithMany("ProjectTasksAssignedUser")
                        .HasForeignKey("AssignedUserId")
                        .HasConstraintName("FK_ProjectTasks_AspNetUsers");

                    b.HasOne("BackEnd.Models.ProjectTasks", "ParentTask")
                        .WithMany("InverseParentTask")
                        .HasForeignKey("ParentTaskId")
                        .HasConstraintName("FK_ProjectTasks_ProjectTasks");

                    b.HasOne("BackEnd.Models.Projects", "Project")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_ProjectTasks_Projects");

                    b.HasOne("BackEnd.Models.Status", "Status")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_dbo.ProjectTasks_dbo.Status_StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackEnd.Models.TaskTypes", "TaskType")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("TaskTypeId")
                        .HasConstraintName("FK_dbo.ProjectTasks_dbo.TaskTypes_TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackEnd.Models.User", "User")
                        .WithMany("ProjectTasksUser")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ProjectTasks_AspNetUsers1");
                });

            modelBuilder.Entity("BackEnd.Models.UserBoards", b =>
                {
                    b.HasOne("BackEnd.Models.User", "User")
                        .WithMany("UserBoards")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserBoards_AspNetUsers");
                });

            modelBuilder.Entity("BackEnd.Models.UserBoardTasks", b =>
                {
                    b.HasOne("BackEnd.Models.ProjectTasks", "Task")
                        .WithMany("UserBoardTasks")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK_UserBoardTasks_ProjectTasks");

                    b.HasOne("BackEnd.Models.UserBoards", "UserBoard")
                        .WithMany("UserBoardTasks")
                        .HasForeignKey("UserBoardId")
                        .HasConstraintName("FK_UserBoardTasks_UserBoards");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
