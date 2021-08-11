namespace MyShop.Services.Goods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Services.Goods.Models;

    public class GoodsService : IGoodsService
    {
        private readonly MyShopDbContext data;

        public GoodsService(MyShopDbContext data)
            => this.data = data;

        public string Create(
            string Title,
            decimal Price,
            int Pieces,
            string ImageUrl,
            string Description,
            int CategoryId,
            int TownId,
            int merchantId)
        {
            var newGoods = new Goods
            {
                Title = Title,
                Price = Price,
                Pieces = Pieces,
                ImageUrl = ImageUrl,
                Description = Description,
                CategoryId = CategoryId,
                TownId = TownId,
                MerchantId = merchantId
            };
            this.data.Goods.Add(newGoods);
            this.data.SaveChanges();
            return newGoods.Id; 
        }

        public void Edit(
            string Id,
            string Title, 
            decimal Price, 
            int Pieces, 
            string ImageUrl, 
            string Description, 
            int CategoryId, 
            int TownId )
        {
            var goods = this.data.Goods.Find(Id);

            goods.Title = Title;
            goods.Price = Price;
            goods.ImageUrl = ImageUrl;
            goods.Pieces = Pieces;
            goods.CategoryId = CategoryId;
            goods.TownId = TownId;
            goods.Description = Description;

            this.data.SaveChanges();
        }
        public GoodsQueryServiceModel All(
            int goodsPerPage,
            int currentPage,
            int townId, 
            int categoruId, 
            string search 
            )
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

            var goods = this.GetGoods(goodsQuery
                .OrderByDescending(g => g.CreatedOn)
                .Where(g => g.CreatedOn > DateTime.Now.AddDays(-30))
                .Skip((currentPage - 1) * goodsPerPage)
                .Take(goodsPerPage));

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
                    Price = g.Price,
                    CategoryId = g.CategoryId,
                    TownId = g.TownId,
                    MerchantId = g.MerchantId,
                    UserId = g.Merchant.UserId
                }).FirstOrDefault();

        public void Delete(string id)
        {
            var purchases = this.data.Purchases
                .Where(p => p.GoodsId == id);
            var goods = this.data.Goods.First(g=> g.Id == id);
            this.data.Purchases.RemoveRange(purchases);
            this.data.Goods.Remove(goods);
            this.data.SaveChanges();
        }
        public IEnumerable<GoodsServiceModel> MerchantGoods(string id)
            => this.GetGoods(this.data
                .Goods
                .Where(g => g.Merchant.UserId == id));

        public bool GoodsIsByMerchant(string id, int merchantId)
            => this.data.Goods.Any(g => g.Merchant.Id == merchantId && g.Id == id);
        public bool GoodsExist(string id)
            => this.data
                .Goods
                .Any(g => g.Id == id);

        public bool CategoryExist(int categoryId)
            => this.data.Categories.Any(g => g.Id == categoryId);

        public bool TownExist(int townId)
            => this.data.Towns.Any(g => g.Id == townId);

        public int GoodsPieces(string id)
            => this.data
                .Goods
                .Where(g => g.Id == id)
                .Select(g => g.Pieces)
                .FirstOrDefault();

         public IEnumerable<TownServiceModel> AllTowns()
            => this.data
                .Towns
                .Select(t => new TownServiceModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToList();

        public IEnumerable<CategoryServiceModel> AllCategories()
            => this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();
   
        public GoodsServiceModel GoodsById(string id)
            => this.data
                .Goods
                .Where(g => g.Id == id)
                .Select(g=> new GoodsServiceModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title,
                    Price = g.Price,
                    Pieces = g.Pieces
                })
                .FirstOrDefault();

        private IEnumerable<GoodsServiceModel> GetGoods(IQueryable<Goods> goodsQuery)
       => goodsQuery
           .Select(g => new GoodsServiceModel
           {
               Id = g.Id,
               ImageUrl = g.ImageUrl,
               Title = g.Title,
               Price = g.Price,
               CreatedOn = g.CreatedOn
           }).ToList();
    }
}
