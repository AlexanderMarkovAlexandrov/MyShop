namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Infrastructures;
    using MyShop.Models.Goods;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public class GoodsController : Controller
    {
        private readonly MyShopDbContext data;
        public GoodsController(MyShopDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Add() 
        {
            var userId = this.User.GetId();
            var isMerchant = this.data.Merchants.Any(c => c.UserId == userId);
            if (!isMerchant)
            {
                return BadRequest();
            }
            return View(new AddGoodsFormModel
               {
                   Categories = this.GetCategories(),
                   Towns = this.GetTowns()
               });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddGoodsFormModel goods)
        {
            var userId = this.User.GetId();
            var isMerchant = this.data.Merchants.Any(c => c.UserId == userId);
            if (!isMerchant)
            {
                return BadRequest();
            }
            if (!this.data.Categories.Any(g=> g.Id == goods.CategoryId))
            {
                this.ModelState.AddModelError(nameof(goods.CategoryId), "The Category does not exist.");
            }
            if (!this.data.Towns.Any(t=> t.Id == goods.TownId))
            {
                this.ModelState.AddModelError(nameof(goods.TownId), "The Town does not exist.");
            }
            if (!ModelState.IsValid)
            {
                goods.Categories = this.GetCategories();
                goods.Towns = this.GetTowns();
                return View(goods);
            }
            var goodsData = new Goods
            {
                Title = goods.Title,
                Price = goods.Price,
                Pieces = goods.Pieces,
                ImageUrl = goods.ImageUrl,
                Description = goods.Description,
                CategoryId = goods.CategoryId,
                TownId = goods.TownId
            };
            this.data.Goods.Add(goodsData);
            this.data.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public IActionResult All(int Id)
        {
            var goods = this.data
                .Goods
                .Where(g=>g.CategoryId == Id)
                .ToList()
                .OrderByDescending(g => g.CreatedOn)
                .Select(g => new GoodsListeningViewModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();

            return View(goods);
        }

        private IEnumerable<GoodsCategoryViewModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new GoodsCategoryViewModel 
                { 
                    Id = c.Id,
                    Name=c.Name
                })
                .OrderBy(c=>c.Name)
                .ToList();

        private IEnumerable<GoodsTownViewModel> GetTowns()
            => this.data
                .Towns
                .Select(t => new GoodsTownViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToList();
    }
}
