namespace MyShop.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Models;
    using MyShop.Services.Goods;
    using static WebConatants;
    public class HomeController : Controller
    {
        private readonly IGoodsService goods;
        public HomeController(IGoodsService goods) 
            => this.goods = goods;
        public IActionResult Index()
        {
            var goods = this.goods.All(GoodsPerPageConst,CurrentPageConst);

            return View(goods.Goods);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
