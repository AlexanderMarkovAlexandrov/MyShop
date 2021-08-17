namespace MyShop.test.AdminControllers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using MyShop.Areas.Admin.Controllers;
    using MyShop.Areas.Admin.Models;
    using Xunit;
    using static Data.GoodsData;


    public class AdminGoodsControllerTest
    {
        [Fact]
        public void AllShoulReturnViewWithCorectData()
            => MyController<GoodsController>
                .Instance(controller => controller
                        .WithData(TenMockGoods))
                .Calling(c => c.All(new AdminAllGoodsViewModel { }))
                .ShouldReturn()
                .View(view => view.WithModelOfType<AdminAllGoodsViewModel>()
                .Passing(m => m.Goods.ToList().Count == 10));
    }
}
