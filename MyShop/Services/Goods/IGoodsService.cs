using MyShop.Services.Goods.Models;
using System.Collections.Generic;

namespace MyShop.Services.Goods
{
    public interface IGoodsService
    {
        GoodsQueryServiceModel All(
            int townId,
            int categoruId,
            string search,
            int goodsPerPage,
            int currentPage);
        bool IsGoods(string id);
        int GoodsPieces(string id);
        GoodsDetailsServiceModel Details(string id);
        IEnumerable<TownServiceModel> GetTowns();
        IEnumerable<CategoryServiceModel> GetCategories();
    }
}
