namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Models;
    using MyShop.Models.Goods;
    using MyShop.Models.Home;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly MyShopDbContext data;
        public HomeController(MyShopDbContext data) => this.data = data;
        public IActionResult Index()
        {
            var goods = this.data.Goods
                .ToList()
                .OrderByDescending(g => g.CreatedOn)
                .Take(10)
                .Select(g => new GoodsListeningViewModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();

            var goodsCategories = new IndexListeningViewModel
            {
                Categories= GetCategories(),
                Goods= goods
            };
            return View(goodsCategories);
        }

        private IEnumerable<GoodsCategoryViewModel> GetCategories()
         => this.data
             .Categories
             .Select(c => new GoodsCategoryViewModel
             {
                 Id = c.Id,
                 Name = c.Name
             })
             .OrderBy(c => c.Name)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
