using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public partial class ProjectTasks
    {
        public ProjectTasks()
        {
            Comments = new HashSet<Comments>();
            Files = new HashSet<Files>();
            ProjectTaskHistory = new HashSet<ProjectTaskHistory>();
            UserBoardTasks = new HashSet<UserBoardTasks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }
        public string Url { get; set; }
        public int StatusId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime? EstimatedEndsOn { get; set; }
        public int? ParentTaskId { get; set; }
        public int? ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CompletedPercent { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<ProjectTaskHistory> ProjectTaskHistory { get; set; }
        public virtual ICollection<UserBoardTasks> UserBoardTasks { get; set; }
        public virtual ProjectTasks ParentTask { get; set; }
        public virtual ICollection<ProjectTasks> InverseParentTask { get; set; }
        public virtual Projects Project { get; set; }
        public virtual Status Status { get; set; }
        public virtual TaskTypes TaskType { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string AssignedUserId { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }

    }
}
