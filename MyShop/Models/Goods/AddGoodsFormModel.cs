namespace MyShop.Models.Goods
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddGoodsFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; init; }
        [Display (Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }
        [Range(PriceMinValue,PriceMaxValue)]
        public double Price { get; init; }
        [Range(PiecesMinValue, PiecesMaxValue)]
        public int Pieces { get; init; }
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        [Display(Name = "Town")]
        public int TownId { get; init; }
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }
        public IEnumerable<GoodsCategoryViewModel> Categories { get; set; }
        public IEnumerable<GoodsTownViewModel> Towns { get; set; }
    }
}
