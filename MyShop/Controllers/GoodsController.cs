namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Infrastructures;
    using MyShop.Models.Goods;
    using System.Collections.Generic;
    using System.Linq;

    public class GoodsController : Controller
    {
        private readonly MyShopDbContext data;
        public GoodsController(MyShopDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Add() 
        {
            var userId = this.User.GetId();
            var isMerchant = this.data.Merchants.Any(c => c.UserId == userId);
            if (!isMerchant)
            {
                return BadRequest();
            }
            return View(new AddGoodsFormModel
               {
                   Categories = this.GetCategories(),
                   Towns = this.GetTowns()
               });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddGoodsFormModel goods)
        {
            var userId = this.User.GetId();
            var merchant = this.data.Merchants.FirstOrDefault(c => c.UserId == userId);
            if (merchant == null)
            {
                return BadRequest();
            }
            if (!this.data.Categories.Any(g=> g.Id == goods.CategoryId))
            {
                this.ModelState.AddModelError(nameof(goods.CategoryId), "The Category does not exist.");
            }
            if (!this.data.Towns.Any(t=> t.Id == goods.TownId))
            {
                this.ModelState.AddModelError(nameof(goods.TownId), "The Town does not exist.");
            }
            if (!ModelState.IsValid)
            {
                goods.Categories = this.GetCategories();
                goods.Towns = this.GetTowns();
                return View(goods);
            }
            var goodsData = new Goods
            {
                Title = goods.Title,
                Price = goods.Price,
                Pieces = goods.Pieces,
                ImageUrl = goods.ImageUrl,
                Description = goods.Description,
                CategoryId = goods.CategoryId,
                TownId = goods.TownId,
                MerchantId = merchant.Id
            };
            this.data.Goods.Add(goodsData);
            this.data.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public IActionResult All(int id , [FromQuery]AllGoodsViewModel query)
        {
            var goodsQuery = this.data.Goods.AsQueryable();
         
            if (!this.data.Categories.Any(c=>c.Id == id))
            {
                return BadRequest();
            }
            goodsQuery = goodsQuery.Where(c => c.CategoryId == id);
            if (query.Search != null)
            {
                goodsQuery = goodsQuery.Where(c => c.Title.Contains(query.Search));
            }
            var goods = goodsQuery
                .OrderByDescending(g => g.CreatedOn)
                .Select(g => new GoodsListeningViewModel
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title
                }).ToList();
            query.Search = null;
            query.Goods = goods;
            query.Category = this.data.Categories.First(c => c.Id == id).Name;
            return View(query);
        }

        public IActionResult Details(string id)
        {
            var goods = this.data
                .Goods
                .Where(g => g.Id == id)
                .FirstOrDefault();
            if (goods == null)
            {
                return BadRequest();
            }
            var goodsData = new GoodsDetailsViewModel
            {
                Id = goods.Id,
                Title = goods.Title,
                ImageUrl = goods.ImageUrl,
                Description = goods.Description,
                Pieces = 0,
                Price = goods.Price
            };
            return View(goodsData);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(GoodsDetailsViewModel goods)
        {
            var goodsData = this.data
                .Goods
                .Where(g => g.Id == goods.Id)
                .FirstOrDefault();
            if (goods == null)
            {
                return BadRequest();
            }
            goods.Title = goodsData.Title;
            goods.Price = goodsData.Price;
            goods.ImageUrl = goodsData.ImageUrl;
            goods.Description = goodsData.Description;
            if (goods.Pieces == 0)
            {
                this.ModelState.AddModelError(nameof(goods.Pieces), "Pieces not must be zero.");
                return View(goods);
            }
            if (goods.Pieces> goodsData.Pieces)
            {
                this.ModelState.AddModelError(nameof(goods.Pieces), "Pieces are more than the available.Write to Merchant.");
                return View(goods);
            }
            var purchase = new Purchase
            {
                GoodsId = goodsData.Id,
                Pieces = goods.Pieces,
                BuyerId = this.User.GetId()
            };
            goodsData.Pieces -= goods.Pieces;
            this.data.Purchases.Add(purchase);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        private IEnumerable<GoodsCategoryViewModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new GoodsCategoryViewModel 
                { 
                    Id = c.Id,
                    Name=c.Name
                })
                .OrderBy(c=>c.Name)
                .ToList();

        private IEnumerable<GoodsTownViewModel> GetTowns()
            => this.data
                .Towns
                .Select(t => new GoodsTownViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToList();
    }
}
