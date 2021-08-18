namespace MyShop.test.Route
{
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using MyShop.Models.Goods;
    using Xunit;

    public class GoodsControllerTest
    {
        [Fact]
        public void AllShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Goods/All")
                    .To<GoodsController>(c => c.All(new AllGoodsViewModel { }));

        [Fact]
        public void AddShouldMapCorectRoute()
           => MyRouting
                    .Configuration()
                    .ShouldMap("/Goods/Add")
                    .To<GoodsController>(c => c.Add());

        [Fact]
        public void PostAddShouldMapCorectRoute()
          => MyRouting
                   .Configuration()
                   .ShouldMap(request => request
                        .WithMethod(HttpMethod.Post)
                        .WithLocation("/Goods/Add"))
                   .To<GoodsController>(c => c.Add(new GoodsFormModel { }));

        [Fact]
        public void EditShouldMapCorectRoute()
           => MyRouting
                    .Configuration()
                    .ShouldMap("/Goods/Edit/1")
                    .To<GoodsController>(c => c.Edit("1"));

        [Fact]
        public void PostEditShouldMapCorectRoute()
          => MyRouting
                   .Configuration()
                   .ShouldMap(request => request
                        .WithMethod(HttpMethod.Post)
                        .WithLocation("/Goods/Edit/1"))
                   .To<GoodsController>(c => c.Edit("1", new GoodsFormModel { }));

        [Fact]
        public void DetailsShouldMapCorectRoute()
          => MyRouting
                   .Configuration()
                   .ShouldMap("/Goods/Details/1")
                   .To<GoodsController>(c => c.Details("1"));
        [Fact]
        public void DeleteShouldMapCorectRoute()
        => MyRouting
                 .Configuration()
                 .ShouldMap("/Goods/Delete/1")
                 .To<GoodsController>(c => c.Delete("1"));

        [Fact]
        public void DeleteConfirmShouldMapCorectRoute()
       => MyRouting
                .Configuration()
                .ShouldMap("/Goods/DeleteConfirm/1")
                .To<GoodsController>(c => c.DeleteConfirm("1"));
    }
}
