namespace MyShop.test.AdminRoutes
{
    using MyTested.AspNetCore.Mvc;
    using MyShop.Areas.Admin.Controllers;
    using MyShop.Areas.Admin.Models;
    using Xunit;
    public class AdminGoodsControllerTest
    {
        [Fact]
        public void AllShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Admin/Goods/All")
                    .To<GoodsController>(c => c.All(new AdminAllGoodsViewModel { }));
    }
}
