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
            => MyController<HomeController>
                .Instance(controller => controller
                        .WithData(TenMockGoods))
                .Calling(c=> c.Index())
                .ShouldReturn()
                .View(view => view.WithModelOfType<IEnumerable<GoodsServiceModel>>()
                .Passing(m=>m.ToList().Count == 4));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
