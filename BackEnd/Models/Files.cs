using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public partial class Files
    {
        public int? TaskId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime? Uploaded { get; set; }
        public bool? IsDeleted { get; set; }
        public int Id { get; set; }

        public virtual ProjectTasks Task { get; set; }
    }
}
