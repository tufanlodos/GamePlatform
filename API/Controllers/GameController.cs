using System.Linq;
using DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors] //bu policy yi atlatmak için.
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //bu authorize zaten giriş yapmış kişiler için. eğer burda bearer ı tanıtmasak identity ile karışırdı. AutSch sonrası policy ve roller de konabilirdi. usingde bir çok şey çıktı ama doğruları :
    //using Microsoft.AspNetCore.Authentication.JwtBearer;
    //using Microsoft.AspNetCore.Authorization;
    public class GameController : ControllerBase
    {
        private readonly GamePlatformDbContext _context;

        public GameController(
            GamePlatformDbContext context)
        {
            _context = context;
        }

        public ActionResult Get()
        {
            var model = _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.GameCategories)
                    .ThenInclude(gc => gc.Category)
                .ToList();

            return Ok(new { sonuc = "bana da ilginç geldi"});
        }
    }
}