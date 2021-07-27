namespace MyShop.Services.Goods.Models
{
    public class GoodsDetailsServiceModel : GoodsServiceModel
    {
        public int Pieces { get; set; }

        public string Description { get; set; }

        public int MerchantId { get; init; }
    }
}
