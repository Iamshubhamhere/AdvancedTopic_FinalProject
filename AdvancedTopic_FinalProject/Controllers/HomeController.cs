using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvancedTopicsAuthDemo.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ATAuthDemoContext _context;
        private readonly UserManager<DemoUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<DemoUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ATAuthDemoContext context, RoleManager<IdentityRole> roleManager, UserManager<DemoUser> userManager, SignInManager<DemoUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            string roleName = "Administrator";

            bool roleExists = await _roleManager.FindByNameAsync(roleName) != null;

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetInvestigatorRole()
        {
            string roleName = "Administrator";

            DemoUser loggedIn = _context.Users.FirstOrDefault(u => User.Identity.Name == u.UserName);

            if(loggedIn == null)
            {
                return Problem("User not found");
            }

            bool hasRoleAlready = await _userManager.IsInRoleAsync(loggedIn, roleName);

            if (!hasRoleAlready)
            {
                await _userManager.AddToRoleAsync(loggedIn, roleName);
                await _signInManager.RefreshSignInAsync(loggedIn);

            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}