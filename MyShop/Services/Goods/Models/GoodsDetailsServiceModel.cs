namespace MyShop.Services.Goods.Models
{
    public class GoodsDetailsServiceModel : GoodsServiceModel
    {
        public string Description { get; set; }
        public int MerchantId { get; init; }
        public int CategoryId { get; init; }
        public int TownId { get; init; }
        public string Merchant { get; init; }
        public string PhoneNumber { get; init; }
        public string UserId { get; init; }
    }
}
