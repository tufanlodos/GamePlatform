using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelUI.Areas.Editor.Models;

namespace PersonelUI.Areas.Editor.Controllers
{
    [Authorize(Roles = "admin,editor")]
    [Area("editor")]
    public class GameController : Controller
    {
        private readonly GamePlatformDbContext _context;

        public GameController(GamePlatformDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Games
                .Include(game => game.Developer)
                .Include(game => game.GameCategories)
                    .ThenInclude(gc => gc.Category)
                .ToList();

            return View(model);
        }

        public IActionResult Add()
        {
            var model = new GameAddGetVm
            {
                Companies = _context.Companies.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(GameAddPostVm data)
        {
            _context.Games.Add(new Game
            {
                GameName = data.GameName,
                DeveloperId = data.DeveloperId,
                IsSafeContent = true,
                PublisherId = data.PublisherId,
                IsActive = true,
                AddedDate = DateTime.Now,
                Price = data.Price,
                PublishDate = DateTime.Now.AddYears(-5)
            });

            _context.SaveChanges();

            var lastGameId = _context.Games
                .OrderByDescending(o => o.AddedDate)
                .FirstOrDefault()
                .Id;

            foreach (var categoryId in data.Categories)
            {
                _context.GameCategories.Add(new GameCategory
                {
                    GameId = lastGameId,
                    CategoryId = categoryId,
                    IsGenre = true
                });
            }

            _context.SaveChanges();

            return Json("Oyun başarıyla eklendi.");
        }

        public IActionResult MakeDiscount(int id)
        {
            var game = _context.Games.Find(id);

            return View(new MakeDiscountVm
            {
                GameId = game.Id,
                CurrentPrice = game.Price
            });
        }

        [HttpPost]
        public async Task<IActionResult> MakeDiscount(int gameId, decimal discountPrice)
        {
            var game = _context.Games.Find(gameId);
            game.DiscountPrice = discountPrice;
            _context.Update(game);
            await _context.SaveChangesAsync();

            return Json("İndirim uygulandı.");
        }

        public async Task<IActionResult> RemoveDiscount(int gameId)
        {
            var game = _context.Games.Find(gameId);
            game.DiscountPrice = null;
            _context.Update(game);
            await _context.SaveChangesAsync();

            return Json("İndirim kaldırıldı.");
        }
    }
}