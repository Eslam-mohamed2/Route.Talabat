using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Application.Abstraction.Models.Auth;
using Route.Talabat.Core.Domain.Entities.Identity;

namespace Route.Talabat.Dashboard.Controllers
{
    public class AdminController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invaild Email");
                return RedirectToAction(nameof(Login));
            }
            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "You Are Not Authorized");
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
