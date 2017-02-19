using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    public class ProjectsAPIController : Controller
    {
        private BugtrackContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectsAPIController(BugtrackContext context, RoleManager<IdentityRole> roleManager,
                                     UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            /*_context.Status.Add(new Status
            {
                Name = "Status1"
            });
            _context.TaskTypes.Add(new TaskTypes
            {
                Name = "TaskType1"
            });
            _context.TaskTypes.Add(new TaskTypes
            {
                Name = "TaskType2"
            });
            _context.SaveChanges();*/
        }

        // GET: api/values
        [Authorize]
        [HttpGet]
        public async Task<string> GetProjects()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //System.Diagnostics.Debug.WriteLine(username);
            var user = await _userManager.FindByNameAsync(username);
            var roleNames = await _userManager.GetRolesAsync(user);
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(roleNames));
            List<Projects> projects = new List<Projects>();

            List<ProjectRoles> projectRoles = await _context.ProjectRoles.ToListAsync();
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(projectRoles));
            
            foreach (var roleName in roleNames)
            {
                foreach (ProjectRoles projectRole in projectRoles)
                {
                    var role = await _roleManager.FindByIdAsync(projectRole.RoleId);
                    Projects project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectRole.ProjectId);
                    if (roleName == role.Name)
                        projects.Add(project);
                }
            }
            //System.Diagnostics.Debug.WriteLine("njk " + JsonConvert.SerializeObject(projects));

            projects = projects.Where(p => p.ParentId == null).ToList();

            JSONDynamicContract contract = new JSONDynamicContract();
            contract.IncludeProperties(new List<string>(){ "Id", "Name", "InverseParent", "ParentId" });
            string serializedProjects = JsonConvert.SerializeObject(projects, Formatting.Indented,
                                            new JsonSerializerSettings { ContractResolver = contract });
            return serializedProjects;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody]Projects project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProject(int id, [FromBody]Projects modifiedProject)
        {
            _context.Projects.Update(modifiedProject);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            Projects project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            await DeleteAllSubprojects(project);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task DeleteAllSubprojects(Projects project)
        {
            List<Projects> projects = await _context.Projects.ToListAsync();
            foreach (Projects childProject in projects)
            {
                if (childProject.ParentId == project.Id)
                    await DeleteAllSubprojects(childProject);
            }

            List<ProjectTasks> tasks = await _context.ProjectTasks.ToListAsync();
            foreach (ProjectTasks task in tasks)
            {
                if (task.ProjectId != project.Id) continue;
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
                foreach (ProjectTaskHistory h in task.ProjectTaskHistory)
                {
                    if (h.TaskId == task.Id)
                        _context.ProjectTaskHistory.Remove(h);
                }
                _context.ProjectTasks.Remove(task);
            }
            
            _context.Projects.Remove(project);
        }
    }
}
