namespace MyShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Comment
    {
        [Key]
        [Required]
        [MaxLength(GuidIdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string  ChatId { get; init; } 
        public Chat Chat { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; init; }
    }
}