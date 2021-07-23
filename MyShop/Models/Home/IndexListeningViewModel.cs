namespace MyShop.Models.Home
{
    using MyShop.Models.Goods;
    using System.Collections.Generic;
    public class IndexListeningViewModel
    {
        public IEnumerable<GoodsListeningViewModel> Goods { get; init; }
    }
}
