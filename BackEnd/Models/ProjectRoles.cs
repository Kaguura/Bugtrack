using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class ProjectRoles
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public virtual Projects Project { get; set; }
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}
