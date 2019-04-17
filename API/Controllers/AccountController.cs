using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Security;
using Entities.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]//bunu ekliyoruz, controllerbase dediği mvc controller ın view desteklemeyen hali sadece.
    [EnableCors] //default u çeker
    //[EnableCors(PolicyName ="myotherpolicy")] gibisinden startuptaki başka bir şey de verilebilr
    //[Produces("application/json")] yazmaya bile gerek kalmadı. ama şu bir gerçek ki yapılacak req ler json olmalı çünkü default öyle ayarlı
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("token")] //route prefix yerine direk içine. yani bura için api/token
        public async Task<ActionResult> GetToken(SignInVm data)
        {

            var user = await _userManager.FindByNameAsync(data.UserName);

            if (user == null)
                return BadRequest(); //bad req controller base den geliyor.

            var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false); //false lockout u devre dışı bırakmada

            if (!result.Succeeded)
                return BadRequest();

            //kullanıcımızsa ona token üretip verme prosedürü işlemeye başlar:
            var signingCredentials = new SigningCredentials(KeyProvider.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: "system", //startupta belirlediğimiz
                audience: "reader", //startupta belirledğimiz
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(new
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token)
            });
        }
        //[HttpGet("createuser")]
        //public async Task<ActionResult> CreateUser()
        //{
        //    //var user = new AppUser
        //    //{
        //    //    UserName = "defuser",
        //    //    FirstName = "default",
        //    //    LastName = "user",
        //    //    Email = "defuser@gmail.com",
        //    //    BirthDay = DateTime.Now.AddYears(-20),
        //    //    SecurityStamp = Guid.NewGuid().ToString()
        //    //};
        //    //await _userManager.CreateAsync(user);
        //    //await _userManager.AddToRoleAsync(user, "DEFAULT_USER");
        //    var user = await _userManager.FindByEmailAsync("defuser@gmail.com");
        //    await _userManager.AddPasswordAsync(user, "123Qw.");
        //    return Ok(new
        //    {
        //        Respond = "its all good"
        //    });
        //}
        //[HttpGet("createrole")]
        //public async Task<ActionResult> CreateRole()
        //{
        //    var role = new AppRole
        //    {
        //        Name = "default_user",
        //        NormalizedName = "DEFAULT_USER",
        //        ConcurrencyStamp = Guid.NewGuid().ToString()
        //    };
        //    await _roleManager.CreateAsync(role);
        //    return Ok(new
        //    {
        //        Respondi = "role created successfully"
        //    });
        //}
    }
}