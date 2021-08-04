namespace MyShop.Services.Goods
{
    using System.Collections.Generic;
    using MyShop.Services.Goods.Models;

    public interface IGoodsService
    {
        public GoodsQueryServiceModel All(
            int townId,
            int categoruId,
            string search,
            int goodsPerPage,
            int currentPage);

        public string Create(
            string Title,
            decimal Price,
            int Pieces,
            string ImageUrl,
            string Description,
            int CategoryId,
            int TownId,
            int merchantId);

        public void Edit(
            string Id,
            string Title,
            decimal Price,
            int Pieces,
            string ImageUrl,
            string Description,
            int CategoryId,
            int TownId);

        public bool GoodsExist(string id);
        public bool GoodsIsByMerchant(string id, int merchantId);
        public int GoodsPieces(string id);
        public bool CategoryExist(int categoriId);
        public bool TownExist(int townId);
        public GoodsDetailsServiceModel Details(string id);
        public GoodsServiceModel GoodsById(string id);
        public IEnumerable<GoodsServiceModel> MerchantGoods(string userId); 
        public IEnumerable<TownServiceModel> AllTowns();
        public IEnumerable<CategoryServiceModel> AllCategories();
    }
}
