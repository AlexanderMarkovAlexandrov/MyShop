namespace MyShop.Models.Chat
{
    using System.Collections.Generic;
    using MyShop.Models.Goods;

    public class ChatViewModel
    {
        public string Id { get; init; }
        public GoodsListeningViewModel Goods { get; init; }
        public IEnumerable<CommentViewModel> Comments { get; init; }
    }
}
