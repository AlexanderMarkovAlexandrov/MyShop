namespace MyShop.Models.Home
{
    using MyShop.Models.Goods;
    using System.Collections.Generic;
    public class IndexListeningViewModel
    {
        public string Search { get; set; }
        public IEnumerable<GoodsCategoryViewModel> Categories { get; init; }

        public IEnumerable<GoodsListeningViewModel> Goods { get; init; }
    }
}
