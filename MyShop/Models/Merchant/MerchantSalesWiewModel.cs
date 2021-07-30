namespace MyShop.Models.Merchant
{
    using System.Collections.Generic;
    using MyShop.Services.Purchase.Models;
    public class MerchantSalesWiewModel
    {
        public IEnumerable<PurchaseServiceModel> Goods { get; init; }

        public decimal TotalSalesAmount { get; init; }
    }
}
