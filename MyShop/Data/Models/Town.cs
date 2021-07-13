namespace MyShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Town
    {
        [Key]
        public int Id { get; init; } 
        [Required]
        [MaxLength(TownMaxLength)]
        public string Name { get; init; }
        public ICollection<Goods> Goods { get; init; } = new List<Goods>();
    }
}