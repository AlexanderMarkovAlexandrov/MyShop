namespace MyShop.Models.Chat
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CommentViewModel
    {
        public string ChatId { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; init; }
    }
}
