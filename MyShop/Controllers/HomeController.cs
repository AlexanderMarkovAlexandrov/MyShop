namespace MyShop.Controllers
{
    using System;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyShop.Models;
    using MyShop.Services.Goods;
    using MyShop.Services.Goods.Models;
    using static WebConatants;
    public class HomeController : Controller
    {
        private readonly IGoodsService goods;
        private IMemoryCache cache;
        public HomeController(IGoodsService goods, IMemoryCache cache)
        {
            this.goods = goods;
            this.cache = cache;
        }
        public IActionResult Index()
        {
            const string LatestGoods = "LatestGoodsCache";

            var goods = this.cache.Get<GoodsQueryServiceModel>(LatestGoods);
            if (goods == null)
            {
                goods = this.goods.All(GoodsPerPageConst, CurrentPageConst);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                this.cache.Set(LatestGoods, goods, cacheOption);
            }

            return View(goods.Goods);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
