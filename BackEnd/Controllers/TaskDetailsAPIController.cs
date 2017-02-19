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
    public class TaskDetailsAPIController : Controller
    {
        private BugtrackContext _context;

        public TaskDetailsAPIController(BugtrackContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> GetTask(int id)
        {
            ProjectTasks task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "Title", "StartedOn", "EndedOn",
                                                            "Url", "StatusId", "TaskTypeId",
                                                            "AssignedUserId", "EstimatedEndsOn",
                                                            "ParentTaskId", "ProjectId",
                                                             "UserId", "Description", "CreatedOn",
                                                             "CompletedPercent"});
            string serializedTask = JsonConvert.SerializeObject(task, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            System.Diagnostics.Debug.WriteLine(serializedTask);
            return serializedTask;
        }

        [HttpGet("{id}")]
        public async Task<string> GetFiles(int id)
        {
            List<Files> files = await _context.Files.ToListAsync();
            files = files.Where(f => f.TaskId == id).ToList();

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "FileName" });
            string serializedFiles = JsonConvert.SerializeObject(files, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            return serializedFiles;
        }

        [HttpGet("{id}")]
        public async Task<string> GetComments(int id)
        {
            List<Comments> comments = await _context.Comments.ToListAsync();
            comments = comments.Where(c => c.ProjectTaskId == id).ToList();

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "Text", "CreateDate", "UserId" });
            string serializedComments = JsonConvert.SerializeObject(comments, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            return serializedComments;
        }

        [HttpGet]
        public async Task<string> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            List<Object> list = new List<Object>();
            foreach (var user in users)
            {
                list.Add(new { Id = user.Id, UserName = user.UserName, Email = user.Email });
            }
            /*
            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "UserName", "Email" });
            string serializedUsers = JsonConvert.SerializeObject(list, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });*/
            string serializedUsers = JsonConvert.SerializeObject(list, Formatting.Indented);
            return serializedUsers;
        }

        [HttpGet]
        public async Task<string> GetStatus()
        {
            List<Status> status = await _context.Status.ToListAsync();

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "Name" });
            string serializedStatus = JsonConvert.SerializeObject(status, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            return serializedStatus;
        }

        [HttpGet]
        public async Task<string> GetTaskTypes()
        {
            List<TaskTypes> taskTypes = await _context.TaskTypes.ToListAsync();

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>() { "Id", "Name" });
            string serializedTaskTypes = JsonConvert.SerializeObject(taskTypes, Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = contract });
            return serializedTaskTypes;
        }

        // POST api/values
        [HttpPost]
        public async Task AddTask([FromBody]ProjectTasks task)
        {
            _context.ProjectTasks.Add(task);
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(task));
            _context.SaveChanges();
            int id = (await _context.ProjectTasks.LastAsync()).Id;
            System.Diagnostics.Debug.WriteLine("id" + id);
            await SaveHistory(id);
        }

        [HttpPost]
        public async Task AddFile([FromBody]Files file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task AddComment([FromBody]Comments comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody]ProjectTasks task)
        {
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
            await SaveHistory(id);
            return Ok();
        }
        /*
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            ProjectTasks task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return NotFound();

            foreach (Files f in task.Files)
            {
                _context.Files.Remove(f);
            }

            foreach (Comments c in task.Comments)
            {
                _context.Comments.Remove(c);
            }

            foreach (ProjectTaskHistory h in task.ProjectTaskHistory)
            {
                _context.ProjectTaskHistory.Remove(h);
            }

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok();
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            Files file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
            if (file == null) return NotFound();
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Comments comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return NotFound();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task SaveHistory(int taskId)
        {
            ProjectTasks task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);

            ProjectTaskHistory history = new ProjectTaskHistory();

            history.AssignedUserId = task.AssignedUserId;
            history.CompletedPercent = task.CompletedPercent;
            history.Description = task.Description;
            history.StartedOn = task.StartedOn;
            history.EndedOn = task.EndedOn;
            history.EstimatedEndsOn = task.EstimatedEndsOn;
            history.ParentTaskId = task.ParentTaskId;
            history.ProjectId = task.ProjectId;
            history.StatusId = task.StatusId;
            history.TaskId = task.Id;
            history.TaskTypeId = task.TaskTypeId;
            history.Title = task.Title;
            history.UserId = task.UserId;

            history.ChangedOn = DateTime.Now;
            _context.ProjectTaskHistory.Add(history);
            await _context.SaveChangesAsync();
        }
    }
}
