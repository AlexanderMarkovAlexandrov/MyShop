namespace MyShop.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Models.Chat;
    using MyShop.Models.Goods;

    public class ChatController : Controller
    {
        private readonly MyShopDbContext data;
        public ChatController(MyShopDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Index(string id)
        {
            var goods = this.data.Goods
                .Where(g => g.Id == id)
                .FirstOrDefault();
            if (goods == null)
            {
                return BadRequest();
            }
            var chat = new ChatViewModel
            {
                Goods = new GoodsListeningViewModel
                {
                    Id = goods.Id,
                    ImageUrl = goods.ImageUrl,
                    Title = goods.Title
                },
                Comments = new List<CommentViewModel>()
               
            };
            return View(chat);
        }
    }
}
