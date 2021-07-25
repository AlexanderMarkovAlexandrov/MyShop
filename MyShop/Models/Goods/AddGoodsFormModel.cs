namespace MyShop.Models.Goods
{
    using MyShop.Services.Goods.Models;
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
        public decimal Price { get; init; }
        [Range(PiecesMinValue, PiecesMaxValue)]
        public int Pieces { get; init; }
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        [Display(Name = "Town")]
        public int TownId { get; init; }
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }
        public IEnumerable<CategoryCerviceModel> Categories { get; set; }
        public IEnumerable<TownServiceModel> Towns { get; set; }
    }
}
