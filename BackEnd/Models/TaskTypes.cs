using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class TaskTypes
    {
        public TaskTypes()
        {
            ProjectTaskHistory = new HashSet<ProjectTaskHistory>();
            ProjectTasks = new HashSet<ProjectTasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectTaskHistory> ProjectTaskHistory { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
    }
}
