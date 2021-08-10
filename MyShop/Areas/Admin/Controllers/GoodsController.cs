namespace MyShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GoodsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
