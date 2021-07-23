using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Goods
{
    public class AllGoodsViewModel
    {
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
