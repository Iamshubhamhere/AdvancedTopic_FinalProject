using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTopic_FinalProject.Controllers
{
    [Authorize(Roles = "Project Manager")]
    public class TaskController : Controller
    {

        private readonly ILogger<TaskController> _logger;
        private readonly ATAuthDemoContext _context;
        private readonly UserManager<DemoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<DemoUser> _signInManager;

        public TaskController(ILogger<TaskController> logger, ATAuthDemoContext context, RoleManager<IdentityRole> roleManager, UserManager<DemoUser> userManager, SignInManager<DemoUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int projectId)
        {
            // Pass the project ID to the view
            ViewData["ProjectId"] = projectId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int projectId, [Bind("Title,RequiredHours,Priority")] Taask task)
        {
            if (ModelState.IsValid)
            {
                // Set the ProjectId of the task to associate it with the project
                task.ProjectId = projectId;

                _context.Taasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Project");
            }

            // If validation fails, pass the project ID back to the view
            ViewData["ProjectId"] = projectId;
            return View(task);
        }

        public async Task<IActionResult> Delete(int id, int Taskid)
        {

            var taask = await _context.Taasks.FirstOrDefaultAsync(t => t.Id == Taskid && t.ProjectId == id);
            _context.Taasks.Remove(taask);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Project");
        }

    }
}