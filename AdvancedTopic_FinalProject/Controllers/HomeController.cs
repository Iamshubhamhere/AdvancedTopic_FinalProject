using AdvancedTopic_FinalProject.Areas.Identity.Data;
using AdvancedTopic_FinalProject.Data;
using AdvancedTopic_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvancedTopic_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskManagementContext _context;


        private readonly UserManager<MainUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<MainUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<MainUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TaskManagementContext journalsContext,
            SignInManager<MainUser> signInManager)
        {
            
            _logger = logger;
            _context = journalsContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            string roleName = "Investigator";

            bool roleExists = await _roleManager.FindByNameAsync(roleName) != null;

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return View();
        }

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