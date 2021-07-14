namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
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

            return Redirect("Goods/All");
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
                }).ToList();

        private IEnumerable<GoodsTownViewModel> GetTowns()
            => this.data
                   .Towns
                   .Select(t => new GoodsTownViewModel
                   {
                       Id = t.Id,
                       Name = t.Name
                   }).ToList();
                
      
    }
}
