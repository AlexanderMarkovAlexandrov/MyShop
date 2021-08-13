namespace MyShop.test.Controlers
{
    using MyShop.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class MerchantControllerTest
    {
        [Fact]
        public void CreatePageShouldReturnViewForRegistredUsers()
                => MyMvc
                    .Pipeline()
                    .ShouldMap(request => request
                            .WithPath("/Merchant/Create")
                            .WithUser())
                    .To<MerchantController>(c => c.Create())
                    .Which()
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View();
    }
}
