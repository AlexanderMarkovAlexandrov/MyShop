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
       
        public int Create(string name, string phoneNumber, string userId)
        {
            var newMarchant = new Merchant
            {
                Name = name,
                PhoneNumber = phoneNumber,
                UserId = userId,
            };

            this.data.Merchants.Add(newMarchant);
            this.data.SaveChanges();
            return newMarchant.Id;
        }

        public int MerchantIdByUser(string userId)
           => this.data.Merchants
               .Where(c => c.UserId == userId)
               .Select(c => c.Id)
               .FirstOrDefault();

        public bool IsMerchantName(string name)
            => this.data.Merchants.Any(c => c.Name == name);

        public bool isMrchantPhoneNember(string phoneNumber)
            => this.data.Merchants.Any(c => c.PhoneNumber == phoneNumber);

        public decimal TotalSalesAmount(int merchantId)
           => this.data.Purchases
                .Where(p => p.Goods.MerchantId == merchantId)
                .Sum(p => p.Amount);
    }
}
