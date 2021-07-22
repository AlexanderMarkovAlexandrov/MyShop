using System.Collections.Generic;

namespace MyShop.Models.Goods
{
    public class AllGoodsViewModel
    {
        public string Category { get; set; }
        public string Search { get; set; }
        public IEnumerable<GoodsListeningViewModel> Goods { get; set; }
    }
}
