namespace MyShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class MyShopDbContext : IdentityDbContext
    {
        public MyShopDbContext(DbContextOptions<MyShopDbContext> options)
            : base(options)
        {
        }
    }
}
