namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Infrastructures;
    using MyShop.Models.Merchant;
    using MyShop.Services.Goods;
    using MyShop.Services.Merchant;
    using MyShop.Services.Purchase;

    public class MerchantController : Controller
    {
        private readonly IGoodsService goods;
        private readonly IPurchaseService purchase;
        private readonly IMerchantService merchant;
        public MerchantController(IMerchantService merchant, IPurchaseService purchase, IGoodsService goods)
        {
            this.merchant = merchant;
            this.goods = goods;
            this.purchase = purchase;
        }


        [Authorize]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateMerchantFormModel merchant)
        {
            var isMerchant = this.merchant.MerchantIdByUser(this.User.GetId());
            var isMerchantName = this.merchant.IsMerchantName(merchant.Name);
            var isMerchantPhone = this.merchant.IsMerchantName(merchant.PhoneNumber);
            if (isMerchant != 0)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant allready exist.");
            }
            if (isMerchantName)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant whit this Name allready exist.");
            }
            if (isMerchantPhone)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant whit this Phone Number allready exist.");
            }
            if (!ModelState.IsValid)
            {
                return View(merchant);
            }

            this.merchant.Create(
                merchant.Name,
                merchant.PhoneNumber,
                this.User.GetId()
                );

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyGoods()
        {
            var userId = this.User.GetId();
            var goods = this.goods.MerchantGoods(userId);
            if (User.IsAdmin())
            {
                return RedirectToAction("All", "Goods");
            }
            return View(goods);
        }
        [Authorize]
        public IActionResult MySales()
        {
            var userId = this.User.GetId();

            return View();
        }
    }
}
