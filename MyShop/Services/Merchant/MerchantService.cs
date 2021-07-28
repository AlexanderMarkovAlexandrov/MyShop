namespace MyShop.Services.Merchant
{
    using System.Linq;
    using MyShop.Data;
    using MyShop.Data.Models;

    public class MerchantService : IMerchantService
    {
        private readonly MyShopDbContext data;

        public MerchantService(MyShopDbContext data)
            => this.data = data;
        public int MerchantIdByUser(string userId)
            => this.data.Merchants
                .Where(c => c.UserId == userId)
                .Select(c=> c.Id)
                .FirstOrDefault();
    }
}
