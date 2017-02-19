using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public partial class ProjectTaskHistory
    {
        public int TaskId { get; set; }
        
        public DateTime ChangedOn { get; set; }
        public DateTime? EstimatedEndsOn { get; set; }

        public string AssignedUserId { get; set; }

        public DateTime? StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }
        public int? TaskTypeId { get; set; }
        public string Title { get; set; }
        public int? StatusId { get; set; }
        public int? ParentTaskId { get; set; }
        public int? ProjectId { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int? CompletedPercent { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Status Status { get; set; }
        public virtual ProjectTasks Task { get; set; }
        public virtual TaskTypes TaskType { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
