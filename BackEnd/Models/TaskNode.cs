using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class TaskNode
    {
        public TaskNode()
        {
            children = new List<TaskNode>();
        }
        public TaskNodeData data { get; set; }
        /*public string title { get; set; }
        public string description { get; set; }*/
        public List<TaskNode> children { get; set; }

        public class TaskNodeData
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
    }
}
