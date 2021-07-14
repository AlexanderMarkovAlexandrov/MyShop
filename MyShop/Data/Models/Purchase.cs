﻿namespace MyShop.Data.Models
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
        [Range(0,MaxPieces)]
        public int Pieces { get; set; }
        [Required]
        public string GoodsId { get; init; }
        public Goods Goods { get; init; }
        [Required]
        public string BuyerId { get; init; }
        public User Buyer { get; init; }
    }
}