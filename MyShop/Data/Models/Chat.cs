namespace MyShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Chat
    {
        [Key]
        [Required]
        [MaxLength(GuidIdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string GoodsId { get; init; }
        public Goods Goods { get; init; }
        public ICollection<Comment> Comments { get; init; } = new List<Comment>();

    }
}