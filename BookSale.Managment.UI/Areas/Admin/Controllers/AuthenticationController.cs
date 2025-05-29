using BookSale.Management.Application;
using BookSale.Management.Domain.Entities;
using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationController(IAuthenticationService authenticationService, SignInManager<ApplicationUser> signInManager)
        {
            _authenticationService = authenticationService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var mdLogin = new LoginModel();
            //ViewBag.Error = TempData["Error"];
            //ViewBag.RemainingSeconds = TempData["RemainingSeconds"];
            //ViewBag.Errors = TempData["Errors"];
            return View(mdLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                                  .Select(x => x.ErrorMessage).ToList();

                ViewBag.Errors = string.Join("<br/>", errors);

                return View(loginModel);
                //return RedirectToAction("Login");

            }

            var result = await _authenticationService.CheckLogin(loginModel.Username, loginModel.Password, loginModel.Remember);

            if (!result.Status)
            {
                ViewBag.Error = result.Message;
                ViewBag.RemainingSeconds = result.RemainingSeconds;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(loginModel);
            //return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
