namespace MyShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Purchase
    {
        [Key]
        [Required]
        [MaxLength(GuidIdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Range(PiecesMinValue,PiecesMaxValue)]
        public int Pieces { get; init; }
        public decimal Amount { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
        [Required]
        public string BuyerId { get; init; }
        public string Buyer { get; init; }
        [Required]
        public string GoodsId { get; init; }
        public Goods Goods { get; init; }
    }
}