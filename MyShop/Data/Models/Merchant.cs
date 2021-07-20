namespace MyShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Merchant
    {
        [Key]
        public int Id { get; init; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserId { get; init; }
        public IEnumerable<Goods> Goods { get; init; } = new List<Goods>();
    }
}
