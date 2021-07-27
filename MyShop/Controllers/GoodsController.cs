namespace MyShop.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Infrastructures;
    using MyShop.Models.Goods;
    using MyShop.Services.Goods;
    using MyShop.Services.Goods.Models;
    using MyShop.Services.Purchase;

    public class GoodsController : Controller
    {
        private readonly IGoodsService goods;
        private readonly MyShopDbContext data;
        private readonly IPurchaseService purchase;
        public GoodsController(MyShopDbContext data, IGoodsService goods, IPurchaseService purchase)
        {
            this.purchase = purchase;
            this.goods = goods;
            this.data = data;
        }
           

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
                Categories = this.goods.GetCategories(),
                Towns = this.goods.GetTowns()
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
            if (!this.data.Categories.Any(g => g.Id == goods.CategoryId))
            {
                this.ModelState.AddModelError(nameof(goods.CategoryId), "The Category does not exist.");
            }
            if (!this.data.Towns.Any(t => t.Id == goods.TownId))
            {
                this.ModelState.AddModelError(nameof(goods.TownId), "The Town does not exist.");
            }
            if (!ModelState.IsValid)
            {
                goods.Categories = this.goods.GetCategories();
                goods.Towns = this.goods.GetTowns();
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All([FromQuery] AllGoodsViewModel query)
        {
            var goodsQuery = this.goods.All(
            query.TownId ,
            query.CategoryId,
            query.Search,
            query.GoodsPerPage,
            query.CurrentPage
                );
           
            query.Goods = goodsQuery.Goods;
            query.TotalGoods = goodsQuery.TotalGoods;
            query.Towns = this.goods.GetTowns();
            query.Categories = this.goods.GetCategories();
            return View(query);
        }

        public IActionResult Details(string id)
        {
            var goodsDetails = this.goods.Details(id);
            if (goodsDetails == null)
            {
                return BadRequest();
            }
           
            return View(goodsDetails);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(GoodsDetailsServiceModel currGoods)
        {
            
            if (!this.goods.IsGoods(currGoods.Id) )
            {
                return BadRequest();
            }
            if (currGoods.Pieces == 0)
            {
                this.ModelState.AddModelError(nameof(currGoods.Pieces), "Pieces not must be zero.");
                return View(currGoods);
            }
            if (currGoods.Pieces > this.goods.GoodsPieces(currGoods.Id))
            {
                this.ModelState.AddModelError(nameof(currGoods.Pieces), "Pieces are more than the available.");
                return View(currGoods);
            }

            var userId = this.User.GetId();
            var result = this.purchase.Create(currGoods.Id, userId, currGoods.Pieces);

            return RedirectToAction("Index", "Home");
        }
    }
}
