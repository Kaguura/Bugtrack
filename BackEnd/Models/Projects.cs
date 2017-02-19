using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Projects
    {
        public Projects()
        {
            ProjectRoles = new HashSet<ProjectRoles>();
            ProjectTaskHistory = new HashSet<ProjectTaskHistory>();
            ProjectTasks = new HashSet<ProjectTasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
        public virtual ICollection<ProjectTaskHistory> ProjectTaskHistory { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
        public virtual Projects Parent { get; set; }
        public virtual ICollection<Projects> InverseParent { get; set; }
    }
}
