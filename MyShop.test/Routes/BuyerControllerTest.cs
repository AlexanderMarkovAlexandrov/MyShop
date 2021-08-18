namespace MyShop.test.Route
{
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Models.Purchase;
    using Xunit;

    public class BuyerControllerTest
    {
        [Fact]
        public void MyPurchasesShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Buyer/MyPurchases")
                    .To<BuyerController>(c => c.MyPurchases());

        [Fact]
        public void BuyShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Buyer/Buy/1")
                    .To<BuyerController>(c => c.Buy("1"));

        [Fact]
        public void PostBuyShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap(request => request
                            .WithMethod(HttpMethod.Post)
                            .WithPath("/Buyer/Buy/1"))
                    .To<BuyerController>(c => c.Buy("1", new PurchaseViewModel { }));
    }
}
