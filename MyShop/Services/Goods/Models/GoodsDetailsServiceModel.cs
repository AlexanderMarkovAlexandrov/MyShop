namespace MyShop.Services.Goods.Models
{
    public class GoodsDetailsServiceModel : GoodsServiceModel
    {
        public string Description { get; set; }
        public int CategoryId { get; init; }
        public int TownId { get; init; }
        public int MerchantId { get; init; }

        public string UserId { get; init; }
    }
}
