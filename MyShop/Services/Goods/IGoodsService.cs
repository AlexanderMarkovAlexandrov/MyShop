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
        IEnumerable<TownServiceModel> GetTowns();
        IEnumerable<CategoryCerviceModel> GetCategories();
    }
}
