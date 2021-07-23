namespace MyShop.Services.Goods
{
    using System.Collections.Generic;
    using System.Linq;
    using MyShop.Data;
    using MyShop.Services.Goods.Models;

    public class GoodsService : IGoodsService
    {
        private readonly MyShopDbContext data;

        public GoodsService(MyShopDbContext data)
            => this.data = data;

        public GoodsQueryServiceModel All(
            int townId, 
            int categoruId, 
            string search, 
            int goodsPerPage,
            int currentPage)
        {
            var goodsQuery = this.data.Goods.AsQueryable();

            if (this.data.Towns.Any(c => c.Id ==townId))
            {
                goodsQuery = goodsQuery.Where(c => c.TownId == townId);
            }
            if (this.data.Categories.Any(c => c.Id == categoruId))
            {
                goodsQuery = goodsQuery.Where(c => c.CategoryId == categoruId);
            }
            if (search != null)
            {
                goodsQuery = goodsQuery.Where(c => c.Title.Contains(search));
            }

            var goods = goodsQuery
                .OrderByDescending(g => g.CreatedOn)
                .Skip((currentPage - 1) * goodsPerPage)
                .Take(goodsPerPage)
                .Select(g => new CoodsServiceModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();

            return new GoodsQueryServiceModel
            {
                TotalGoods = goodsQuery.Count(),
                CurrentPage = currentPage,
                GoodsPerPage = goodsPerPage,
                Search = search,
                Goods = goods
            };
        }
        public IEnumerable<TownServiceModel> GetTowns()
        {
           return this.data
                .Towns
                .Select(t => new TownServiceModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToList();
        }
        public IEnumerable<CategoryCerviceModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new CategoryCerviceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();
    }
}
