using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class Role : IdentityRole
    {
        public Role(string name) : base(name)
        {

        }
        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }

    }
}
