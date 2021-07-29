namespace MyShop.Services.Merchant
{
    using MyShop.Data.Models;
    public interface IMerchantService
    {
        public int MerchantIdByUser(string userId);

        public bool IsMerchantName(string name);

        public bool isMrchantPhoneNember(string phoneNumber);

        public int Create(string name, string phoneNumber, string userId);
    }
}
