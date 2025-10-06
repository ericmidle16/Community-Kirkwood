using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentation.Models;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            // new to instantiate identity manager classes
            _userManager = userManager;
            _signInManager = signInManager;

            _logger = logger;
        }

        private LogicLayer.IUserManager _userManagerLogic = new UserManager();
        private Models.AccessToken _accessToken = new Models.AccessToken("");

        private void getAccessToken()
        {
            if (_signInManager.IsSignedIn(User))
            {
                string email = User.Identity.Name;
                try
                {
                    _accessToken = new Models.AccessToken(email);
                }
                catch { }
            }
            else
            {
                return;
            }
        }

        public async Task<IActionResult> Index()
        {
            getAccessToken();
            if(_accessToken.IsLoggedIn)
            {
                var id = _userManager.GetUserId(User);
                var users = _userManager.Users;
                var u = users.First(u => u.Id == id);

                foreach (var role in _accessToken.Roles)
                {
                    if (!User.IsInRole(role))
                    {
                        await _userManager.AddToRoleAsync(u, role);
                    }
                }
            }
            return RedirectToAction("Welcome");
        }

        public IActionResult Welcome()
        {
            getAccessToken();
            if(_accessToken.IsLoggedIn)
            {
                ViewBag.Name = _accessToken.GivenName + " " +
                    _accessToken.FamilyName + "!";
            }
            return View("Welcome");
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

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}