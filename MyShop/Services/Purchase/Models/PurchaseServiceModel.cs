namespace MyShop.Services.Purchase.Models
{
    using System;
    public class PurchaseServiceModel
    {
        public string Id { get; init; } 
        public int Pieces { get; init; }
        public decimal Amount { get; init; }
        public DateTime CreatedOn { get; init; } 
        public string BuyerName { get; init; }
        public string GoodsTitle { get; init; }
        public string GoodsImg { get; init; }
    }
}
