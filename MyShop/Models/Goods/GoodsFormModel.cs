namespace MyShop.Models.Goods
{
    using MyShop.Services.Goods.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class GoodsFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }
        [Display (Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }
        [Range(PriceMinValue,PriceMaxValue)]
        public decimal Price { get; set; }
        [Range(PiecesMinValue, PiecesMaxValue)]
        public int Pieces { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Town")]
        public int TownId { get; set; }
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
        public IEnumerable<CategoryServiceModel> Categories { get; set; }
        public IEnumerable<TownServiceModel> Towns { get; set; }
    }
}
