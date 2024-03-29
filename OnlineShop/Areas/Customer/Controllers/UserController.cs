﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Customer.Controllers
{

    [Area("Customer")]
    [Authorize]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public UserController(UserManager<IdentityUser>userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
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
                    var isSaveRole = await _userManager.AddToRoleAsync(user, "User");
                    TempData["save"] = "Account has been created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }


        public async Task<IActionResult> Edit(string? id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c =>c.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);           
        }

        [HttpPost]
        public async Task<IActionResult>Edit(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if(userInfo == null)
            {
                return NotFound();
            }
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["save"] = "Information has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        public async Task<IActionResult> Details(string? id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        public async Task<IActionResult> Lockout(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();  
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Lockout(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c=> c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = DateTime.Now.AddYears(100);
            int rowAffected =  _db.SaveChanges();
            if(rowAffected > 0)
            {
                TempData["save"] = "User's lockout has been excuted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }


        public async Task<IActionResult>Active(string? id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been unlocked successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            _db.ApplicationUsers.Remove(userInfo);  
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["delete"] = "User has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
    }
}
