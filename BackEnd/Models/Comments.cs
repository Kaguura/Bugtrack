using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProjectTaskId { get; set; }
        public string UserId { get; set; }

        public virtual ProjectTasks ProjectTask { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
