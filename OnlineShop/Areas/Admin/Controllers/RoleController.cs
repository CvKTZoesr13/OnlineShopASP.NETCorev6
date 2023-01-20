using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Data;
using System.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RoleController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _db;
        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string? name)
        {
            IdentityRole role = new();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.msg = "This role is already exist!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been created successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        public async Task<IActionResult> Edit(string? id)   
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string? id, string? name)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.msg = "This role is already exist!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // Delete Action Method
        public async Task<IActionResult> Delete(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }
        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View();  
        }


        public async Task<IActionResult> Assign()
        {
            ViewData["UserId"]= new SelectList(_db.ApplicationUsers.Where(f=>f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList(), "Id", "UserName");
            ViewData["RoleId"]= new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVm roleUser)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == roleUser.UserId);
            var isCheckRoleAssign =await _userManager.IsInRoleAsync(user, roleUser.RoleId);
            if(isCheckRoleAssign)
            {
                ViewBag.message = "Ops, This user is already assigned to this role!";
                ViewData["UserId"]= new SelectList(_db.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewData["RoleId"]= new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }

            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "Role has been assigned successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public ActionResult AssignUserRole()
        {
            var result = from user in _db.UserRoles
                         join role in _db.Roles
                         on user.RoleId equals role.Id
                         join a in _db.ApplicationUsers on user.UserId equals
                         a.Id
                         select new UserRoleMaping()
                         {
                             UserId = user.UserId,
                             RoleId = user.RoleId,
                             UserName = a.UserName,
                             RoleName = role.Name

                         };
            ViewBag.UserRoles = result;
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Delete(string? id, string? name)
        //{

        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    role.Name = name;
        //    var isExist = await _roleManager.RoleExistsAsync(role.Name);
        //    if (isExist)
        //    {
        //        ViewBag.msg = "This role is already exist!";
        //        ViewBag.name = name;
        //        return View();
        //    }
        //    var result = await _roleManager.UpdateAsync(role);
        //    if (result.Succeeded)
        //    {
        //        TempData["save"] = "Role has been updated successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View();
        //}
    }
}
