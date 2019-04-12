using System.Linq;
using GamePlatform.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Web.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : ControllerBase {

        private readonly GamePlatformDbContext _context;

        public GameController(
            GamePlatformDbContext context) {
            _context = context;
        }

        public ActionResult Get() {
            var model = _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .ToList();

            return Ok(model);
        }
    }
}