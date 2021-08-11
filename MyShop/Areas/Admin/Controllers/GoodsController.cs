namespace MyShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Services.Goods;
    using static AdminConstants;

    public class GoodsController : AdminController
    {
        private readonly IGoodsService goods;

        public GoodsController(IGoodsService goods)
        {
            this.goods = goods;
        }

        public IActionResult All()
        {
            var goods = this.goods.All(GoodsPerPageConst, CurrentPageConst).Goods;
            return View(goods);
        }
    }
}
