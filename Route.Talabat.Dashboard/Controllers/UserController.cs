using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Controllers
{
    public class UserController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    DisplayName = user.DisplayName,
                    PhoneNumber = user.PhoneNumber!,
                    Email = user.Email!,
                    Roles = roles
                });
            }

            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var User = await _userManager.FindByIdAsync(Id);
            var AllRoles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel()
            {
                UserId = User!.Id,
                UserName = User.UserName!,
                Roles = AllRoles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name!,
                    IsSelected = _userManager.IsInRoleAsync(User, r.Name).Result
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Id, UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
