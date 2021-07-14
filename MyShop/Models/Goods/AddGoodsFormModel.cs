namespace MyShop.Models.Goods
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddGoodsFormModel
    {
        public string Title { get; init; }
        [Display (Name ="Image URL")]
        public string ImageUrl { get; init; }
        public double Price { get; init; }
        public int Pieces { get; init; }
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        [Display(Name = "Town")]
        public int TownId { get; init; }
        public string Description { get; init; }
        public IEnumerable<GoodsCategoryViewModel> Categories { get; set; }
        public IEnumerable<GoodsTownViewModel> Towns { get; set; }
    }
}
