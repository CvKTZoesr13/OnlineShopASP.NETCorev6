using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser>userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }


        // add Create Action Method
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST Create Action Method
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    TempData["save"] = "Created account successfully!";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
    }
}
