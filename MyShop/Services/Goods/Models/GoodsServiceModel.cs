namespace MyShop.Services.Goods.Models
{
    using System;
    public class GoodsServiceModel
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string ImageUrl { get; init; }
        public decimal Price { get; init; }
        public int Pieces { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
