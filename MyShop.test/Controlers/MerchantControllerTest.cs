namespace MyShop.test.Controlers
{
    using System.Collections.Generic;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Data.Models;
    using MyShop.Models.Merchant;
    using MyShop.Services.Goods.Models;
    using Xunit;
    using static Data.GoodsData;

    public class MerchantControllerTest
    {
        [Fact]
        public void CreateShouldReturnViewForRegistredUsers()
            => MyController<MerchantController>
                .Instance(controller => controller
                            .WithUser())
                .Calling(c => c.Create())
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
        public void PostCreateShouldReturnViewWithInvalidData()
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(Merchant(TestUser.Identifier))))
                    .Calling(c => c.Create(new CreateMerchantFormModel { }))
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<CreateMerchantFormModel>());
                    

        [Fact]
        public void MyGoodsShouldReturnViewWithCorectData()
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(Merchant(TestUser.Identifier),
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
                            .WithUser()
                            .WithData(data => data
                                .WithEntities(Merchant(TestUser.Identifier),
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

        [Fact]
        public void MySalesShouldReturnUnauthorizedIfUserIsNotMerchant()
            => MyController<MerchantController>
                    .Instance(controller => controller
                            .WithUser())
                    .Calling(c => c.MySales())
                    .ShouldReturn()
                    .Unauthorized();
    }
}
