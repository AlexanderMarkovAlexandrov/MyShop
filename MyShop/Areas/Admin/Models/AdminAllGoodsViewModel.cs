namespace MyShop.Areas.Admin.Models
{
    using System.Collections.Generic;
    using MyShop.Services.Goods.Models;

    public class AdminAllGoodsViewModel
    {
        public int GoodsPerPage { get; set; } = 10;
        public int TotalGoods { get; set; }
        public int CurrentPage { get; set; } = 1;
        public IEnumerable<GoodsServiceModel> Goods { get; set; }
    }
}
