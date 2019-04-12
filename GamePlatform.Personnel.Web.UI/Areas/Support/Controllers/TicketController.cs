using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlatform.DAL.Context;
using GamePlatform.Entities.Entity;
using GamePlatform.Personnel.Web.UI.Areas.Support.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Personnel.Web.UI.Areas.Support.Controllers {
    [Area("support")]
    [Authorize(Roles = "admin,support")]
    public class TicketController : Controller {
        private readonly GamePlatformDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TicketController(GamePlatformDbContext context,
            UserManager<AppUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string status) {
            var baseModel = _context.SupportTicket
                .Include(st => st.SupportTicketCategory)
                .Include(st => st.IssuedUser)
                .Include(st => st.FixedUser);

            List<SupportTicket> model;

            switch (status) {
                case "all":
                case null:
                    model = baseModel.ToList();
                    break;
                case "unanswered":
                    model = baseModel.Where(x => x.IsSolved == null).ToList();
                    break;
                default:
                    model = baseModel.ToList();
                    break;
            }

            return View(model);
        }

        public IActionResult Details(int ticketId) {
            var ticket = _context.SupportTicket.FirstOrDefault(x => x.Id == ticketId);

            var relatedSteps = _context.SupportTicketSteps
                .Where(x => x.SupportTicketId == ticketId)
                .Include(sts => sts.SupportTicket)
                .Include(sts => sts.AnsweredUser)
                .ToList();

            var model = new TicketDetailsVm {
                SupportTicket = ticket,
                SupportTicketSteps = relatedSteps
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerTicket(TicketAnswerVm data) {

            _context.SupportTicketSteps.Add(new SupportTicketStep {
                AddedDate = DateTime.Now,
                AnsweredUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id,
                Content = data.Content,
                SupportTicketId = data.TicketId,
                IsActive = true
            });

            await _context.SaveChangesAsync();

            return Json("Cevap iletildi");
        }
    }
}