namespace MyShop.Services.Purchase
{
    public interface IPurchaseService
    {
        public string Create(string goodsId, string buyerId, int pieces);
    }
}
