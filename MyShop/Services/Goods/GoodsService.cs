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
                .Select(g => new GoodsServiceModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title,
                    Price = g.Price
                }).ToList();

            return new GoodsQueryServiceModel
            {
                TotalGoods = goodsQuery.Count(),
                Goods = goods
            };
        }

        public GoodsDetailsServiceModel Details(string id)
            => this.data
                .Goods
                .Where(g => g.Id == id)
                .Select(g => new GoodsDetailsServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Pieces = 0,
                    Price = g.Price,
                    MerchantId = g.MerchantId
                }).FirstOrDefault();

        public bool IsGoods(string id)
            => this.data
                .Goods
                .Any(g => g.Id == id);

        public int GoodsPieces(string id)
            => this.data
                .Goods
                .Where(g => g.Id == id)
                .Select(g => g.Pieces)
                .FirstOrDefault();
         public IEnumerable<TownServiceModel> GetTowns()
            => this.data
                .Towns
                .Select(t => new TownServiceModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToList();

        public IEnumerable<CategoryServiceModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();
    }
}
