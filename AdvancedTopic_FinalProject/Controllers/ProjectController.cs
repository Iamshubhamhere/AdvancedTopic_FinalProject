using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Security.Claims;

namespace AdvancedTopic_FinalProject.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProjectController : Controller
    {

        private readonly ILogger<ProjectController> _logger;
        private readonly ATAuthDemoContext _context;
        private readonly UserManager<DemoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<DemoUser> _signInManager;

        public ProjectController(ILogger<ProjectController> logger, ATAuthDemoContext context, RoleManager<IdentityRole> roleManager, UserManager<DemoUser> userManager, SignInManager<DemoUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public IActionResult Index(string? sortBy, int? sortId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

           
            var projects = _context.Projects.Include(project => project.Tasksids).OrderBy(project => project.title).ToList();

            if (string.IsNullOrEmpty(sortBy))
            {
               
            }
            else if (sortBy == "RequiredHours")
            {
                if (sortId.HasValue)
                {
                    
                    var projectToSort = projects.FirstOrDefault(project => project.Id == sortId.Value);

                    if (projectToSort != null)
                    {
                       
                        projectToSort.Tasksids = new HashSet<Taask>(
                            projectToSort.Tasksids.OrderBy(task => task.RequiredHours)
                        );
                    }
                }
            }
            else if (sortBy == "Priority")
            {
                if (sortId.HasValue)
                {
                    
                    var projectToSort = projects.FirstOrDefault(project => project.Id == sortId.Value);

                    if (projectToSort != null)
                    {
                        
                        projectToSort.Tasksids = new HashSet<Taask>(
                            projectToSort.Tasksids.OrderBy(task => task.Priority)
                        );
                    }
                }
            }

            var developerRoleId = _context.Roles
                .Where(role => role.Name == "Developer")
                .Select(role => role.Id)
                .FirstOrDefault();

            var developerUserIds = _context.UserRoles
                .Where(userRole => userRole.RoleId == developerRoleId)
                .Select(userRole => userRole.UserId)
                .ToList();

            





            return View(projects);
        }




        public IActionResult Create()
        {


            var RoleD = _roleManager.Roles.FirstOrDefault(r => r.Name == "Developer");
            var developers = _context.UserRoles
                .Where(userRole => userRole.RoleId == RoleD.Id)
                .ToList();
            var userIds = developers.Select(userRole => userRole.UserId).ToList();
            var users = _context.Users.Where(user => userIds.Contains(user.Id)).ToList();

            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, List<string> AreChecked)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var project = new Project
            {
                title = title,
                DemoUserID = userId 
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            foreach (var selectedUserId in AreChecked)
            {
                var demoUserProject = new DemoUserProject
                {
                    UserId = selectedUserId, 
                    ProjectId = project.Id 
                };

                _context.DemoUserProjects.Add(demoUserProject);
            }

            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p=>p.Id== id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p=>p.Id== id);

            return View(project);
        }

    }
}
