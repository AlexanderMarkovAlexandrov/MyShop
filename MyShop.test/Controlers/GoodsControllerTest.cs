namespace MyShop.test.Controlers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Data.Models;
    using MyShop.Models.Goods;
    using MyShop.Services.Goods.Models;
    using Xunit;
    using static Data.GoodsData;


    public class GoodsControllerTest
    {
        [Fact]
        public void AllShoulReturnViewWithCorectData()
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithData(TenMockGoods))
                .Calling(c => c.All(new AllGoodsViewModel { }))
                .ShouldReturn()
                .View(view => view.WithModelOfType<AllGoodsViewModel>()
                .Passing(m=> m.Goods.ToList().Count == 4));

        [Fact]
        public void AddShouldReturnViewIfUserIsMerchant()
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(Merchant(TestUser.Identifier)))
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<GoodsFormModel>());

        [Theory]
        [InlineData("Title", 1, 2, "https://myshop.com/png/200.png", "Description of goods", 1, 1)]
        public void PostAddShouldCreateGoodsAndRedirectToCorectAction(
            string title,
            int categoryId,
            int townId,
            string img,
            string description,
            decimal price,
            int pieces)
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(data => data
                            .WithEntities(Merchant(TestUser.Identifier),
                                          new Category { Id = categoryId },
                                          new Town { Id = townId })))
                .Calling(c => c.Add(new GoodsFormModel
                {
                    Title = title,
                    TownId = townId,
                    CategoryId = categoryId,
                    ImageUrl = img,
                    Description = description,
                    Pieces = pieces,
                    Price = price
                }))
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                        .WithSet<Goods>(goods => goods
                        .Any(g => g.Title == title)))
                .AndAlso()
                .ShouldReturn()
                .Redirect();

        [Theory]
        [InlineData("GoodsId")]
        public void EditShouldReturnViewWithCorectData(string goodsId)
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(data => data
                            .WithEntities(Merchant(TestUser.Identifier),
                                          new Category { Id = 1 },
                                          new Town { Id = 1 },
                                          new Goods { Id = goodsId, MerchantId = 1})))
                .Calling(c=> c.Edit(goodsId))
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<GoodsFormModel>());

        [Theory]
        [InlineData("GoodsId", "Title", 1, 2, "https://myshop.com/png/200.png", "Description of goods", 1, 1)]
        public void PostEditShouldEditGoodsWithCorectDataAndRedirectToCorectAction(
            string goodsId,
            string title,
            int categoryId,
            int townId,
            string img,
            string description,
            decimal price,
            int pieces)
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(data => data
                            .WithEntities(Merchant(TestUser.Identifier),
                                          new Category { Id = categoryId },
                                          new Town { Id = townId },
                                          new Goods
                                          {
                                              Id = goodsId,
                                              CategoryId = categoryId,
                                              TownId = townId,
                                              MerchantId = 1
                                          })))
                .Calling(c => c.Edit(goodsId, new GoodsFormModel
                {
                    Title = title,
                    CategoryId = categoryId,
                    TownId = townId,
                    Description = description,
                    ImageUrl = img,
                    Price = price,
                    Pieces = pieces
                }))
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                                .WithSet<Goods>(goods => goods
                                .Any(g => g.Title == title && g.Id == goodsId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect.
                                To<GoodsController>(c=>c.Details(goodsId)));

        [Theory]
        [InlineData("GoodsId")]
        public void DeleteShouldReturnViewWithCorectGoodsAndModel(string goodsId)
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(data => data
                             .WithEntities(Merchant(TestUser.Identifier),
                                           new Goods
                                           {
                                               Id = goodsId,
                                               MerchantId = 1
                                           })))
                .Calling(c => c.Delete(goodsId))
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<GoodsServiceModel>()
                .Passing(m => m.Id == goodsId));

        [Theory]
        [InlineData("GoodsId")]
        public void DeleteConfirmShouldRemoveGoodsWithCorectDataAndRedirect(string goodsId)
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithUser()
                        .WithData(data => data
                             .WithEntities(Merchant(TestUser.Identifier),
                                           new Goods
                                           {
                                               Id = goodsId,
                                               MerchantId = 1
                                           })))
                .Calling(c => c.DeleteConfirm(goodsId))
                .ShouldHave()
                .ActionAttributes(attributs => attributs
                        .RestrictingForAuthorizedRequests())
                .Data(data => data
                                .WithSet<Goods>(goods => !goods
                                .Any(g => g.Id == goodsId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect();
    }
}
