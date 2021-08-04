namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Infrastructures;
    using MyShop.Models.Purchase;
    using MyShop.Services.Goods;
    using MyShop.Services.Purchase;

    public class BuyerController : Controller
    {
        private readonly IPurchaseService purchase;
        private readonly IGoodsService goods;

        public BuyerController(IPurchaseService purchase , IGoodsService goods)
        {
            this.goods = goods;
            this.purchase = purchase;
        }

        [Authorize]
        public IActionResult MyPurchases()
        {
            var userId = this.User.GetId();
            var goods = this.purchase.PurchasesByBuyer(userId);
            return View(goods);
        }
        [Authorize]
        public IActionResult Buy(string id)
        {
            if (!this.goods.GoodsExist(id))
            {
                return BadRequest();
            }
            var currgoods = goods.GoodsById(id);

            return View(new PurchaseViewModel
            {
                goods = currgoods,
            }); 
        }
        [Authorize]
        [HttpPost]
        public IActionResult Buy(string id, PurchaseViewModel input)
        {
            if (!this.goods.GoodsExist(id))
            {
                return BadRequest();
            }
            var currGoods = goods.GoodsById(id);
            input.goods = currGoods;
            if (input.Pieces == 0)
            {
                this.ModelState.AddModelError(nameof(currGoods.Pieces), "Pieces not must be zero.");
                return View(input);
            }
            if (input.Pieces > this.goods.GoodsPieces(id))
            {
                this.ModelState.AddModelError(nameof(currGoods.Pieces), "Pieces are more than the available.");
                return View(input);
            }

            var userId = this.User.GetId();
            var result = this.purchase.Create(id, userId, input.Pieces);
            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(input);
        }
    }
}
