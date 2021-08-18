namespace MyShop.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Infrastructures;
    using MyShop.Models.Goods;
    using MyShop.Services.Goods;
    using MyShop.Services.Merchant;
    using MyShop.Services.Purchase;
    using static WebConatants;
    public class GoodsController : Controller
    {
        private readonly IGoodsService goods;
        private readonly IPurchaseService purchase;
        private readonly IMerchantService merchant;
        private readonly IMapper mapper;
        public GoodsController(IGoodsService goods, IPurchaseService purchase, IMerchantService merchant, IMapper mapper)
        {
            this.merchant = merchant;
            this.purchase = purchase;
            this.goods = goods;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllGoodsViewModel query)
        {
            var goodsQuery = this.goods.All(
                query.GoodsPerPage,
                query.CurrentPage,
                query.TownId,
                query.CategoryId,
                query.Search
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
            var id = this.goods.Create(
                goods.Title,
                goods.Price,
                goods.Pieces,
                goods.ImageUrl,
                goods.Description,
                goods.CategoryId,
                goods.TownId,
                merchantId
                );

            this.TempData[SuccessMessageKey] = "You succeed added the Goods!";

            return RedirectToAction(nameof(GoodsController.Details), new { id });
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
            var formGoods = this.mapper.Map<GoodsFormModel>(goods);
            formGoods.Categories = this.goods.AllCategories();
            formGoods.Towns = this.goods.AllTowns();
            return View(formGoods);
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
            this.goods.Edit(
            id,
            goods.Title,
            goods.Price,
            goods.Pieces,
            goods.ImageUrl,
            goods.Description,
            goods.CategoryId,
            goods.TownId);

            this.TempData[SuccessMessageKey] = "You succeed edit the Goods!";

            return RedirectToAction(nameof(GoodsController.Details), new { id });
        }

        public IActionResult Details(string id)
        {
            var goodsDetails = this.goods.Details(id);
            if (goodsDetails == null)
            {
                return BadRequest();
            }
            goodsDetails.Pieces = 0;

            return View(goodsDetails);
        }
        [Authorize]
        public IActionResult Delete(string id)
        {
            if (!this.goods.GoodsExist(id))
            {
                return BadRequest();
            }
            var userId = this.User.GetId();
            var merchantId = this.merchant.MerchantIdByUser(userId);
            if (!this.goods.GoodsIsByMerchant(id, merchantId) && !this.User.IsAdmin())
            {
                return BadRequest();
            }
            var goods = this.goods.GoodsById(id);
            return View(goods);
        }

        [Authorize]
        public IActionResult DeleteConfirm(string id)
        {
            if (!this.goods.GoodsExist(id))
            {
                return BadRequest();
            }
            var userId = this.User.GetId();
            var merchantId = this.merchant.MerchantIdByUser(userId);
            if (!this.goods.GoodsIsByMerchant(id, merchantId) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            this.goods.Delete(id);
            return RedirectToAction(nameof(MerchantController.MyGoods), "Merchant");
        }
    }
}
