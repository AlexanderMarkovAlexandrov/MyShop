namespace MyShop.Services.Goods.Models
{
    using System.Collections.Generic;

    public class GoodsQueryServiceModel
    {
        public int GoodsPerPage { get; set; }
        public int TotalGoods { get; set; }
        public int CurrentPage { get; set; }
        public string Search { get; set; }
        public IEnumerable<CoodsServiceModel> Goods { get; set; }
    }
}
