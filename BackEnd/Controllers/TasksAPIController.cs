using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BackEnd.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TasksAPIController : Controller
    {
        private BugtrackContext _context;

        public TasksAPIController(BugtrackContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> GetTasks(int id)
        {
            List<ProjectTasks> tasks = await _context.ProjectTasks.ToListAsync();
            tasks = tasks.Where(t => t.ProjectId == id && t.ParentTaskId == null).ToList();

            List<TaskNode> tasknodes = new List<TaskNode>();
            foreach (ProjectTasks task in tasks)
            {
                TaskNode node = new TaskNode
                {
                    data = new TaskNode.TaskNodeData
                    {
                        id = task.Id,
                        title = task.Title,
                        description = task.Description
                    }
                };
                tasknodes.Add(node);
                AddToChildrenProperty(node, task);
            }

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "id", "data", "title", "description", "children" });

            string serializedTasks = JsonConvert.SerializeObject(tasknodes, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            return serializedTasks;
            //return "{\"data\":[{\"data\":{\"name\":\"Documents\",\"size\":\"75kb\",\"type\":\"Folder\"},\"children\":[{\"data\":{\"name\":\"Work\",\"size\":\"55kb\",\"type\":\"Folder\"}}]}]}";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            ProjectTasks task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return NotFound();
            await DeleteAllSubtasks(task);
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task DeleteAllSubtasks(ProjectTasks task)
        {
            List<ProjectTasks> tasks = await _context.ProjectTasks.ToListAsync();
            foreach (ProjectTasks childTask in tasks)
            {
                if (childTask.ParentTaskId == task.Id)
                    await DeleteAllSubtasks(childTask);
            }

            List<Files> files = await _context.Files.ToListAsync();
            foreach (Files f in files)
            {
                if (f.TaskId == task.Id)
                    _context.Files.Remove(f);
            }
            List<Comments> comments = await _context.Comments.ToListAsync();
            foreach (Comments c in comments)
            {
                if (c.ProjectTaskId == task.Id)
                    _context.Comments.Remove(c);
            }
            List<ProjectTaskHistory> history = await _context.ProjectTaskHistory.ToListAsync();
            foreach (ProjectTaskHistory h in history)
            {
                if (h.TaskId == task.Id)
                    _context.ProjectTaskHistory.Remove(h);
            }
            _context.ProjectTasks.Remove(task);
        }

        private void AddToChildrenProperty(TaskNode node, ProjectTasks task)
        {
            if (task.InverseParentTask == null) return;
            foreach (ProjectTasks child in task.InverseParentTask)
            {
                TaskNode childNode = new TaskNode
                {
                    data = new TaskNode.TaskNodeData { id = child.Id, title = child.Title, description = child.Description }
                };
                node.children.Add(childNode);
                AddToChildrenProperty(childNode, child);
            }
        }
        
    }
}
