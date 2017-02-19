using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd.Models
{
    public partial class UserBoards
    {
        public UserBoards()
        {
            UserBoardTasks = new HashSet<UserBoardTasks>();
        }

        public int Id { get; set; }
        
        public string Title { get; set; }
        public bool? IsArchive { get; set; }

        public virtual ICollection<UserBoardTasks> UserBoardTasks { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
