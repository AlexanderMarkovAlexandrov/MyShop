namespace MyShop.test.Controlers
{
    using MyShop.Controllers;
    using MyShop.Data.Models;
    using MyShop.Models.Merchant;
    using MyShop.Services.Goods.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class MerchantControllerTest
    {
        [Fact]
        public void CreateShouldReturnViewForRegistredUsers()
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

        [Theory]
        [InlineData("merchant", "+3599999999")]
        public void PostCreateShouldBeForAuthorizedUsersAndShuoldCreateMerchantAndRedirect(
            string name, 
            string phoneNumber)
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser())
                    .Calling(c => c.Create(new CreateMerchantFormModel
                    {
                        Name = name,
                        PhoneNumber = phoneNumber
                    }))
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests()
                            .RestrictingForHttpMethod(HttpMethod.Post))
                    .ValidModelState()
                    .Data(data => data
                            .WithSet<Merchant>(merchant => merchant
                            .Any(m =>
                                m.Name == name &&
                                m.PhoneNumber == phoneNumber &&
                                m.UserId == TestUser.Identifier)))
                    .AndAlso()
                    .ShouldReturn()
                    .Redirect(redirect => redirect      
                            .To<HomeController>(c=> c.Index()));
        [Fact]
        public void MyGoodsShouldReturnViewWithCorectData()
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser(TestUser.Identifier)
                            .WithData(data => data
                                .WithEntities(new Merchant { Id = 1, UserId = TestUser.Identifier },
                                              new Goods { MerchantId = 1 })))
                    .Calling(c => c.MyGoods())
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<IEnumerable<GoodsServiceModel>>()
                    .Passing(m => m.ToList().Count == 1));

        [Fact]
        public void MySalesShouldReturnViewWithCorectData()
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser(TestUser.Identifier)
                            .WithData(data => data
                                .WithEntities(new Merchant { Id = 1, UserId = TestUser.Identifier },
                                              new Goods {Id = "goodsId" , MerchantId = 1 },
                                              new Purchase { GoodsId = "goodsId"})))
                    .Calling(c => c.MySales())
                    .ShouldHave()
                    .ActionAttributes(attribute => attribute
                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<MerchantSalesWiewModel>()
                    .Passing(m=> m.Goods.ToList().Count == 1));
    }
}
