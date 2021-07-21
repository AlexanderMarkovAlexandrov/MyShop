namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class ChatController : Controller
    {
        public IActionResult Index(string id)
        {
            return View();
        }
    }
}
