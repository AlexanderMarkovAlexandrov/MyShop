namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class BuyerController : Controller
    {
        public IActionResult MyPurchases()
        {
            return View();
        }
    }
}
