using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlatform.DAL.Context;
using GamePlatform.Entities.Entity;
using GamePlatform.Personnel.Web.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Personnel.Web.UI.Areas.Admin.Controllers {
    [Area("admin")]
    public class UserController : Controller {
        private readonly GamePlatformDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(
            GamePlatformDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public IActionResult Index() {
            return View();
        }

        public IActionResult Add() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddVm data) {
            if (ModelState.IsValid) {
                var user = new AppUser {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    UserName = data.UserName,
                    BirthDay = DateTime.Now.AddDays(-20)
                };

                await _userManager.CreateAsync(user, data.Password);
                await _userManager.AddToRoleAsync(user, data.RoleName);
            }
            return View();
        }
    }
}