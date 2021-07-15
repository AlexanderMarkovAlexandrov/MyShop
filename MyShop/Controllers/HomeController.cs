namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Models;
    using MyShop.Models.Goods;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly MyShopDbContext data;
        public HomeController(MyShopDbContext data) => this.data = data;
        public IActionResult Index()
        {
            var goods = this.data
                .Goods
                .ToList()
                .OrderBy(g => g.CreatedOn)
                .Take(10)
                .Select(g => new GoodsListeningViewModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();



            return View(goods);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
