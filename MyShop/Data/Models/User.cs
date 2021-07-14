namespace MyShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class User
    {
        [Key]
        [Required]
        [MaxLength(GuidIdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(UserNameMaxLength)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }
        [Required]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; }
        public ICollection<Goods> Goods { get; init; }
        public ICollection<Chat> Chats { get; init; }
        public ICollection<Comment> Comments { get; init; }
    }
}