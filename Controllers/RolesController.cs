using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Models;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult RoleIndex()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var exists = await _roleManager.RoleExistsAsync(roleName);
                if (!exists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    return RedirectToAction(nameof(RoleIndex));
                }
                ModelState.AddModelError("", "Role already exists.");
            }
            return View();
        }

        public async Task<IActionResult> EditUsers(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound();

            var model = new RoleUsersViewModel
            {
                RoleName = roleName,
                Users = new System.Collections.Generic.List<UserRoleViewModel>()
            };

            foreach (var user in _userManager.Users)
            {
                var userRoleVM = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await _userManager.IsInRoleAsync(user, roleName)
                };
                model.Users.Add(userRoleVM);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditUsers(RoleUsersViewModel model)
        {
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                return NotFound();

            for (int i = 0; i < model.Users.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model.Users[i].UserId);
                if (user == null) continue;

                if (model.Users[i].IsSelected && !(await _userManager.IsInRoleAsync(user, model.RoleName)))
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                }
                else if (!model.Users[i].IsSelected && await _userManager.IsInRoleAsync(user, model.RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                }
            }
            return RedirectToAction(nameof(RoleIndex));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return BadRequest();

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("RoleIndex", _roleManager.Roles.ToList());
            }

            return RedirectToAction(nameof(RoleIndex));
        }

    }
}