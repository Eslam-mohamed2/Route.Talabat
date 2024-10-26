using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));
            var RoleExists = await _roleManager.RoleExistsAsync(model.Name);
            if (RoleExists)
            {
                ModelState.AddModelError("Name", "Role is Already Exist!");
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            };
            var mappedRole = new IdentityRole(model.Name.Trim());
            await _roleManager.CreateAsync(mappedRole);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string? Id)
        {
            if (Id is null) return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role is null) return NotFound();
            await _roleManager.DeleteAsync(Role);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            if (Id is null) return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role is null) return NotFound();
            var mappedRole = new RoleViewModel()
            {
                Id = Id,
                Name = Role.Name!
            };
            return View(mappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string Id, RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Validation Error");
                return View(model);
            }
            var Role = await _roleManager.FindByIdAsync(model.Id);
            Role!.Name = model.Name;
            await _roleManager.UpdateAsync(Role);
            return RedirectToAction(nameof(Index));
        }
    }

}
