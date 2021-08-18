namespace MyShop.test.Route
{
    using MyTested.AspNetCore.Mvc;
    using MyShop.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldMapCorectRoute()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/")
                    .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorShouldMapCorectRoute()
           => MyRouting
                    .Configuration()
                    .ShouldMap("/Home/Error")
                    .To<HomeController>(c => c.Error());
    }
}
