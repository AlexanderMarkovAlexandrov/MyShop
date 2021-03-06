namespace MyShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Goods
    {
        [Key]
        [Required]
        [MaxLength(GuidIdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Pieces { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int TownId { get; set; }
        public Town Town { get; set; }
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public int MerchantId { get; init; }
        public Merchant Merchant { get; init; }
        public IEnumerable<Purchase> Purchases { get; init; } = new List<Purchase>();
    }
}
