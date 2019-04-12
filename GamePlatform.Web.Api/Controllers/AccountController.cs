using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using GamePlatform.DAL.Context;
using GamePlatform.Entities.Entity;
using GamePlatform.Web.Api.Models;
using GamePlatform.Web.Api.Security;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GamePlatform.Web.Api.Controllers {
    [Route("api")]
    [ApiController]
    //[Produces("application/json")]
    public class AccountController : ControllerBase {

        private readonly GamePlatformDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(
            GamePlatformDbContext context,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager) {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("token")]
        public async Task<ActionResult> GetToken(SignInVm data) {

            var user = await _userManager.FindByNameAsync(data.UserName);

            if (user == null)
                return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

            if (!result.Succeeded)
                return BadRequest();

            var signingCredentials = new SigningCredentials(KeyProvider.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: "system",
                audience: "reader",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(new {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token)
            });
        }
    }
}