using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProjectRoleController : Controller
    {
        private readonly BugtrackContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProjectRoleController(
            BugtrackContext context,
            RoleManager<IdentityRole> roleManager 
        ){
            _context = context;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!_context.Projects.Any())
            {
                _context.Projects.Add(
                    new Projects
                    {
                        Name = "Project 1"
                    });
                await _context.SaveChangesAsync();
            }
            
            return View(await _context.Projects.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.Include(p => p.ProjectRoles).ThenInclude(pr => pr.Role).FirstOrDefaultAsync(p => p.Id == id);

            var allRoles = await _roleManager.Roles.ToListAsync();

            ViewBag.AllRoles = allRoles;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(int projectId, string roleId)
        {
            if (roleId == null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(roleId);
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return NotFound();
            if (role == null) return NotFound();

            if (await _context.ProjectRoles.FirstOrDefaultAsync(pr => pr.ProjectId == projectId && pr.RoleId == roleId) == null)
            {
                _context.ProjectRoles.Add(new ProjectRoles
                {
                    Project = project,
                    Role = role
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", "ProjectRole", new { id = projectId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int projectId, string roleId)
        {
            if (roleId == null) return NotFound();

            var link = await _context.ProjectRoles.FirstOrDefaultAsync(l => l.ProjectId == projectId && l.RoleId == roleId);

            if (link == null) return RedirectToAction("Edit");

            _context.ProjectRoles.Remove(link);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "ProjectRole", new { id = projectId });
        }
    }
}