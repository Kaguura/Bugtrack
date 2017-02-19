using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class UserBoardTasks
    {
        public int UserBoardId { get; set; }
        public int? TaskId { get; set; }
        public int Id { get; set; }

        public virtual ProjectTasks Task { get; set; }
        public virtual UserBoards UserBoard { get; set; }
    }
}
