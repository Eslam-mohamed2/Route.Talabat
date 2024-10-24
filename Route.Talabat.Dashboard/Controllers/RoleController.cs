using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Route.Talabat.Dashboard.Controllers
{
    public class RoleController (RoleManager<IdentityRole> _roleManger) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManger.Roles.ToListAsync();
            return View(roles);
        }
    }
}
