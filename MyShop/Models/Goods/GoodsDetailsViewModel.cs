﻿using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Goods
{
    public class GoodsDetailsViewModel
    {
        public string Id { get; init; } 
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Price:")]
        public decimal Price { get; set; }
        [Display(Name ="Pieces:")]
        public int Pieces { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}