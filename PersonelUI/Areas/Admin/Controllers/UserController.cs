using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonelUI.Areas.Admin.Models;

namespace PersonelUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        /* ROLE EKLEME ALTYAPISI
         public async Task<IActionResult> Index()
        {
            var user = new AppRole
            {
                Name="support",
                NormalizedName="SUPPORT",
                ConcurrencyStamp= Guid.NewGuid().ToString()
            };
            await _roleManager.CreateAsync(user);
            return View();

        }
             */
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddVm data)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
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