namespace MyShop.test.Controlers.MyShopWeb
{
    using System.Collections.Generic;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Data.Models;
    using MyShop.Models.Purchase;
    using MyShop.Services.Purchase.Models;
    using Xunit;

    public class BuyerControllerTest
    {
        [Fact]
        public void MyPurchasesShouldReturnViewWithCorectData()
            => MyController<BuyerController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(new Goods { Id = "goodsId" },
                                              new Purchase { GoodsId = "goodsId", BuyerId = TestUser.Identifier })))
                    .Calling(c => c.MyPurchases())
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<IEnumerable<PurchaseServiceModel>>()
                    .Passing(m => m.ToList().Count == 1));

        [Theory]
        [InlineData("goodsId")]
        public void BuyShouldReturnViewWithCorectData(string goodsId)
            => MyController<BuyerController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(new Goods { Id = goodsId })))
                    .Calling(c => c.Buy(goodsId))
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<PurchaseViewModel>()
                    .Passing(m => m.goods.Id == goodsId));

        [Theory]
        [InlineData("goodsId", 1, 10)]
        public void PostBuyShouldCreatePurchaseAndRedirectToCorectAction(string goodsId, int pieces, decimal price)
            => MyController<BuyerController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(new Goods { Id = goodsId, Pieces = pieces, Price = price })))
                    .Calling(c => c.Buy(goodsId, new PurchaseViewModel { Pieces = pieces }))
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .ValidModelState()
                    .Data(data => data
                            .WithSet<Purchase>(purchase => purchase
                                .Any(p =>
                                     p.BuyerId == TestUser.Identifier &&
                                     p.GoodsId == goodsId &&
                                     p.Pieces == pieces &&
                                     p.Amount == price)))
                    .AndAlso()
                    .ShouldReturn()
                    .Redirect(redirect => redirect
                            .To<HomeController>(c => c.Index()));

        [Theory]
        [InlineData("goodsId", 0)]
        public void PostBuyShouldReturnViewWithZeroPiecesInModel(string goodsId, int pieces)
            => MyController<BuyerController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(new Goods { Id = goodsId, Pieces = 1 })))
                    .Calling(c => c.Buy(goodsId, new PurchaseViewModel { Pieces = pieces }))
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<PurchaseViewModel>());
    }
}
