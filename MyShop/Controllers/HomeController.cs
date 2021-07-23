namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Models;
    using MyShop.Models.Goods;
    using MyShop.Models.Home;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly MyShopDbContext data;
        public HomeController(MyShopDbContext data) 
            => this.data = data;
        public IActionResult Index()
        {
            var goodsData = this.data.Goods
                .ToList()
                .OrderByDescending(g => g.CreatedOn)
                .Take(4)
                .Select(g => new GoodsListeningViewModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();

            var goods = new IndexListeningViewModel
            {
                Goods= goodsData
            };
            return View(goods);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
