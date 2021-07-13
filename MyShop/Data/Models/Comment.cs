namespace MyShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Comment
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string OwnerId { get; init; }
        public User Owner { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; init; }
    }
}