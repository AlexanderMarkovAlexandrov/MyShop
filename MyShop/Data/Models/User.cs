namespace MyShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static Data.DataConstants;
    public class User : IdentityUser
    {
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }
    }
}
