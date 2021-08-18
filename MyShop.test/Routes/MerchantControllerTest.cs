namespace MyShop.test.Route
{
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Models.Merchant;
    using Xunit;

    public class MerchantControllerTest
    {
        [Fact]
        public void CreateShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Merchant/Create")
                    .To<MerchantController>(c => c.Create());

        [Fact]
        public void PostCreateShouldMapCorectRoute()
         => MyRouting
                 .Configuration()
                 .ShouldMap(request => request
                        .WithMethod(HttpMethod.Post)
                        .WithLocation("/Merchant/Create"))
                 .To<MerchantController>(c => c.Create(new CreateMerchantFormModel { } ));

        [Fact]
        public void MyGoodsShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Merchant/MyGoods")
                    .To<MerchantController>(c => c.MyGoods());

        [Fact]
        public void MySalesShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Merchant/MySales")
                    .To<MerchantController>(c => c.MySales());
    }
}
