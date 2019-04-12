using GamePlatform.Personnel.Web.UI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Personnel.Web.UI.Controllers {
    [Authorize(Roles = SystemRoles.All)]
    public class HomeController : Controller {
        public IActionResult Index() {
            
            return View();
        }
    }
}
