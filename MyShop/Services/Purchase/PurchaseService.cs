namespace MyShop.Services.Purchase
{
    using System.Linq;
    using MyShop.Data;
    using MyShop.Data.Models;
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
                Pieces = pieces,
            };

            this.data.Purchases.Add(purchase);
            goods.Pieces -= pieces;
            this.data.SaveChanges();

            return purchase.Id;
        }
    }
}
