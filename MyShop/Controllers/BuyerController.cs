namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Infrastructures;
    using MyShop.Services.Purchase;

    public class BuyerController : Controller
    {
        private readonly IPurchaseService purchase;

        public BuyerController(IPurchaseService purchase)
        {
            this.purchase = purchase;
        }

        [Authorize]
        public IActionResult MyPurchases()
        {
            var userId = this.User.GetId();
            var goods = this.purchase.PurchasesByBuyer(userId);
            return View(goods);
        }
    }
}
