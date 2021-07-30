namespace MyShop.Services.Purchase
{
    using System.Collections.Generic;
    using System.Linq;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Services.Purchase.Models;

    public class PurchaseService : IPurchaseService
    {
        private readonly MyShopDbContext data;

        public PurchaseService(MyShopDbContext data)
            => this.data = data;

        public string Create(string goodsId, string buyerId, int pieces)
        {
            var goods = this.data
                .Goods
                .Where(g => g.Id == goodsId)
                .FirstOrDefault();

            var purchase = new Purchase
            {
                GoodsId = goodsId,
                BuyerId = buyerId,
                Amount = pieces * goods.Price,
                Pieces = pieces,
            };

            this.data.Purchases.Add(purchase);
            goods.Pieces -= pieces;
            this.data.SaveChanges();

            return purchase.Id;
        }
        public IEnumerable<PurchaseServiceModel> PurchasesByBuyer(string userId)
        {
            var purchases = this.data
                .Purchases
                .Where(p => p.BuyerId == userId)
                .Select(p => new PurchaseServiceModel
                {
                    Id = p.Id,
                    Pieces = p.Pieces,
                    Amount = p.Amount,
                    CreatedOn = p.CreatedOn,
                    GoodsTitle = p.Goods.Title,
                    GoodsImg = p.Goods.ImageUrl,
                    BuyerName = p.BuyerId
                })
                .ToList();
            return purchases;
        }
        public IEnumerable<PurchaseServiceModel> PurchasesByMerchant(int merchantId)
        {
            var purchases = this.data
                .Purchases
                .Where(p => p.Goods.MerchantId == merchantId)
                .Select(p => new PurchaseServiceModel
                {
                    Id = p.Id,
                    Pieces = p.Pieces,
                    Amount = p.Amount,
                    CreatedOn = p.CreatedOn,
                    GoodsTitle = p.Goods.Title,
                    GoodsImg = p.Goods.ImageUrl,
                    BuyerName = p.BuyerId
                })
                .ToList();
            return purchases;

        }
    }
}
