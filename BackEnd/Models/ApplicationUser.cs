using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<ProjectTaskHistory> ProjectTaskHistory { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasksAssignedUser { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasksUser { get; set; }
        public virtual ICollection<UserBoards> UserBoards { get; set; }
    }
}
