namespace MyShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Areas.Admin.Models;
    using MyShop.Services.Goods;
    using static AdminConstants;

    public class GoodsController : AdminController
    {
        private readonly IGoodsService goods;

        public GoodsController(IGoodsService goods)
        {
            this.goods = goods;
        }

        public IActionResult All([FromQuery] AdminAllGoodsViewModel query)
        {
            var goodsQuery = this.goods.All(
                query.GoodsPerPage,
                query.CurrentPage
                );

            query.Goods = goodsQuery.Goods;
            query.TotalGoods = goodsQuery.TotalGoods;

            return View(query);
        }
    }
}
