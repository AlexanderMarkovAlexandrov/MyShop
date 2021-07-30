namespace MyShop.Services.Purchase
{
    using System.Collections.Generic;
    using MyShop.Services.Purchase.Models;

    public interface IPurchaseService
    {
        public string Create(string goodsId, string buyerId, int pieces);
        public IEnumerable<PurchaseServiceModel> PurchasesByBuyer(string userId);
        public IEnumerable<PurchaseServiceModel> PurchasesByMerchant(int merchantId);
    }
}
