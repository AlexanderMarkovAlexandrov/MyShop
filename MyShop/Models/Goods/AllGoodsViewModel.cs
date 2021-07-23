namespace MyShop.Models.Goods
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AllGoodsViewModel
    {
        public int GoodsPerPage { get; set; } = 2;
        public int TotalGoods { get; set; }
        public int CurrentPage { get; set; } = 1;

        [Display(Name = "Choose offers from Category")]
        public int CategoryId { get; set; }  

        [Display(Name = "Search by text")]
        public string Search { get; set; }

        [Display(Name = "Choose offers from City")]
        public int TownId { get; set; }
        public IEnumerable<GoodsCategoryViewModel> Categories { get; set; }
        public IEnumerable<GoodsTownViewModel> Towns { get; set; }
        public IEnumerable<GoodsListeningViewModel> Goods { get; set; }
    }
}
