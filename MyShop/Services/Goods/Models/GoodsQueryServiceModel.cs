namespace MyShop.Services.Goods.Models
{
    using System.Collections.Generic;

    public class GoodsQueryServiceModel
    {
        public int TotalGoods { get; set; }
        public IEnumerable<GoodsServiceModel> Goods { get; set; }
    }
}
