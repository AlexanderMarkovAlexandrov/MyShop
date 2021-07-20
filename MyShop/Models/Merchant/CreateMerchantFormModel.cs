namespace MyShop.Models.Merchant
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateMerchantFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [RegularExpression(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$")]
        public string PhoneNumber { get; set; }
    }
}
