namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MerchantController :Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
