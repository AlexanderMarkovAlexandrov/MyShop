namespace MyShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Category
    {
        [Key]
        public int Id { get; init; } 
        [Required]
        [MaxLength(CategoryMaxLength)]
        public string Name { get; init; }
        public IEnumerable<Goods> Goods { get; init; } = new List<Goods>();
    }
}