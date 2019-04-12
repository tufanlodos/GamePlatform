using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlatform.DAL.Context;
using GamePlatform.Entities.Entity;
using GamePlatform.Personnel.Web.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Personnel.Web.UI.Controllers {
    public class AccountController : Controller {
        private readonly GamePlatformDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            GamePlatformDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult SignIn() {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVm data) {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByNameAsync(data.UserName);

                if (user == null)
                    return View();

                var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

                if (!result.Succeeded)
                    return View();

                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> SignOut() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult AccessDenied() {
            return View();
        }

        protected override void Dispose(bool disposing) {
            if (disposing)
                _context.Dispose();
        }
    }
}