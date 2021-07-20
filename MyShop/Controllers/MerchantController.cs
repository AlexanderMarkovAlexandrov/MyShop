namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Data;
    using MyShop.Data.Models;
    using MyShop.Infrastructures;
    using MyShop.Models.Merchant;
    using System.Linq;

    public class MerchantController :Controller
    {
        private readonly MyShopDbContext data;
        public MerchantController(MyShopDbContext data) => this.data = data;
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateMerchantFormModel merchant)
        {
            var isMerchant = this.data.Merchants.Any(c => c.UserId == this.User.GetId());
            var isMerchantName = this.data.Merchants.Any(c => c.Name == merchant.Name);
            var isMerchantPhone = this.data.Merchants.Any(c => c.PhoneNumber == merchant.PhoneNumber);
            if (isMerchant)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant allready exist.");
            }
            if (isMerchantName)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant whit this Name allready exist.");
            }
            if (isMerchantPhone)
            {
                this.ModelState.AddModelError(nameof(merchant), "The Merchant whit this Phone Number allready exist.");
            }
            if (!ModelState.IsValid)
            {
                return View(merchant);
            }

            var newMarchant = new Merchant
            {
                Name = merchant.Name,
                PhoneNumber = merchant.PhoneNumber,
                UserId = this.User.GetId(),
            };

            this.data.Merchants.Add(newMarchant);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
