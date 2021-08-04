using MyShop.Services.Goods.Models;

namespace MyShop.Models.Purchase
{
    public class PurchaseViewModel
    {
        public GoodsServiceModel goods;
        public int Pieces { get; set; }
    }
}
