namespace MyShop.test.Controlers
{
    using System.Collections.Generic;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Services.Goods.Models;
    using Xunit;
    using static Data.GoodsData;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c=> c.Index())
                .Which(controller => controller.WithData(TenMockGoods))
                .ShouldReturn()
                .View(view => view.WithModelOfType<IEnumerable<GoodsServiceModel>>()
                .Passing(m=>m.ToList().Count == 4));

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
