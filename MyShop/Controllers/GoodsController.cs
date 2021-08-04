namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Infrastructures;
    using MyShop.Models.Goods;
    using MyShop.Services.Goods;
    using MyShop.Services.Goods.Models;
    using MyShop.Services.Merchant;
    using MyShop.Services.Purchase;

    public class GoodsController : Controller
    {
        private readonly IGoodsService goods;
        private readonly IPurchaseService purchase;
        private readonly IMerchantService merchant;
        public GoodsController( IGoodsService goods, IPurchaseService purchase, IMerchantService merchant)
        {
            this.merchant = merchant;
            this.purchase = purchase;
            this.goods = goods;
        }

        public IActionResult All([FromQuery] AllGoodsViewModel query)
        {
            var goodsQuery = this.goods.All(
            query.TownId,
            query.CategoryId,
            query.Search,
            query.GoodsPerPage,
            query.CurrentPage
                );

            query.Goods = goodsQuery.Goods;
            query.TotalGoods = goodsQuery.TotalGoods;
            query.Towns = this.goods.AllTowns();
            query.Categories = this.goods.AllCategories();
            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();
            
            if (this.merchant.MerchantIdByUser(userId) == 0)
            {
                return BadRequest();
            }
            return View(new GoodsFormModel
            {
                Categories = this.goods.AllCategories(),
                Towns = this.goods.AllTowns()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(GoodsFormModel goods)
        {
            var userId = this.User.GetId();
            var merchantId = this.merchant.MerchantIdByUser(userId);
            if (merchantId == 0)
            {
                return RedirectToAction(nameof(MerchantController.Create), "Merchant");
            }
            if (!this.goods.CategoryExist(goods.CategoryId))
            {
                this.ModelState.AddModelError(nameof(goods.CategoryId), "The Category does not exist.");
            }
            if (!this.goods.TownExist(goods.TownId))
            {
                this.ModelState.AddModelError(nameof(goods.TownId), "The Town does not exist.");
            }
            if (!ModelState.IsValid)
            {
                goods.Categories = this.goods.AllCategories();
                goods.Towns = this.goods.AllTowns();
                return View(goods);
            }
            this.goods.Create(
                goods.Title,
                goods.Price,
                goods.Pieces,
                goods.ImageUrl,
                goods.Description,
                goods.CategoryId,
                goods.TownId,
                merchantId
                );

            return RedirectToAction("All", "Goods");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var userId = this.User.GetId();

            if (this.merchant.MerchantIdByUser(userId) == 0 && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(MerchantController.Create), "Merchant");
            }

            var goods = this.goods.Details(id);

            if (goods.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }
            return View(new GoodsFormModel
            {
                Title = goods.Title,
                ImageUrl = goods.ImageUrl,
                Price = goods.Price,
                Pieces = goods.Pieces,
                Description = goods.Description,
                CategoryId = goods.CategoryId,
                TownId = goods.TownId,
                Categories = this.goods.AllCategories(),
                Towns = this.goods.AllTowns()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id , GoodsFormModel goods)
        {
            if (!this.goods.GoodsExist(id))
            {
                return BadRequest();
            }
            var userId = this.User.GetId();
            var merchantId = this.merchant.MerchantIdByUser(userId);
            if ( !this.goods.GoodsIsByMerchant(id, merchantId) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            this.goods.Edit(
            id,
            goods.Title,
            goods.Price,
            goods.Pieces,
            goods.ImageUrl,
            goods.Description,
            goods.CategoryId,
            goods.TownId);

            return RedirectToAction("MyGoods", "Merchant");
        }

        public IActionResult Details(string id)
        {
            var goodsDetails = this.goods.Details(id);
            goodsDetails.Pieces = 0;
            if (goodsDetails == null)
            {
                return BadRequest();
            }
           
            return View(goodsDetails);
        }
    }
}
