namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Models.Goods;
    using System.Collections.Generic;
    using System.Linq;

    public class GoodsController : Controller
    {
        private readonly MyShopDbContext data;
        public GoodsController(MyShopDbContext data) => this.data = data;
        public IActionResult Add() => View(new AddGoodsFormModel
        {
            Categories = this.GetCategories(),
            Towns = this.GetTowns()
        });
        
        [HttpPost]
        public IActionResult Add(AddGoodsFormModel goods)
        {
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

            return RedirectToAction("All","Goods");
        }

        public IActionResult All()
        {
            return View();
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
