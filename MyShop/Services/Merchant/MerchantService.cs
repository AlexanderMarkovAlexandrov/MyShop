namespace MyShop.Services.Merchant
{
    using System.Linq;
    using MyShop.Data;

    public class MerchantService : IMerchantService
    {
        private readonly MyShopDbContext data;

        public MerchantService(MyShopDbContext data)
            => this.data = data;
        public bool IsMerchant(string userId)
            => this.data.Merchants.Any(c => c.UserId == userId);
    }
}
