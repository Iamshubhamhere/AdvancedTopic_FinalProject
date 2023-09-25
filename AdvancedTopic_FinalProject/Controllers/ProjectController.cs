﻿using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Security.Claims;
using Project = AdvancedTopicsAuthDemo.Models.Project;

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

            var projectUserNames = new Dictionary<int, List<string>>();

            foreach (var project in projects)
            {
                var userIds = _context.DemoUserProjects
                    .Where(du => du.ProjectId == project.Id)
                    .Select(du => du.UserId)
                    .ToList();

                var userNames = _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .Select(u => u.UserName)
                    .ToList();

                projectUserNames[project.Id] = userNames;
            }

            ViewBag.ProjectUserNames = projectUserNames;


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


        public async Task<IActionResult> AddToDeveloper(int id)
        {
            var userIds = _context.DemoUserProjects
                .Where(du => du.ProjectId == id)
                .Select(du => du.UserId)
                .ToList();

            var RoleId = _roleManager.Roles.FirstOrDefault(r => r.Name == "Developer");

            var developers = _context.UserRoles
                .Where(userRole => userRole.RoleId == RoleId.Id)
                .Select(userRole => userRole.UserId)
                .ToList();

            
            var usersNotInProject = developers.Except(userIds).ToList();


            List<DemoUser> usersList = new List<DemoUser>();

            foreach (var userId in usersNotInProject)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    usersList.Add(user);
                }
            }
            ViewBag.projectName = _context.Projects.FirstOrDefault(p => p.Id == id);
            ViewBag.UsersNotInProject = usersList;
            ViewBag.PId = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToDeveloper(int Pid, List<string> AreChecked)
        {

            foreach (var selectedUserId in AreChecked)
            {
                var demoUserProject = new DemoUserProject
                {
                    UserId = selectedUserId,
                    ProjectId = Pid
                };

                _context.DemoUserProjects.Add(demoUserProject);
            }

            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }


    }
}
