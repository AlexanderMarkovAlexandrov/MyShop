namespace MyShop.Infrastructures
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyShop.Data;
    using MyShop.Data.Models;
    using static MyShop.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;
            var data = serviceProvider.GetRequiredService<MyShopDbContext>();
            data.Database.Migrate();

            SeedCategories(data);
            SeedTowns(data);
            SeedAdministrator(serviceProvider);
            return app;
        }
        private static void SeedCategories(MyShopDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category{ Name = "Electronics"},
                new Category{ Name = "Home"},
                new Category{ Name = "Sport"},
                new Category{ Name = "Fashion"},
                new Category{ Name = "Autos"},                
                new Category{ Name = "Children"},
                new Category{ Name = "Garden"},
                new Category{ Name = "Animals"}
            });

            data.SaveChanges();
        }
        private static void SeedTowns(MyShopDbContext data)
        {
            if (data.Towns.Any())
            {
                return;
            }
            data.Towns.AddRange(new[]
            {
                new Town{Name = "Sofia"},
                new Town{Name = "Plovdiv"},
                new Town{Name = "Varna"},
                new Town{Name = "Burgas"},
                new Town{Name = "Ruse"},
                new Town{Name = "Pleven"},
                new Town{Name = "Vidin"},
                new Town{Name = "Blagoevgrad"},
                new Town{Name = "Sliven"},
                new Town{Name = "Vratsa"},
                new Town{Name = "Pernik"},
                new Town{Name = "Dobrich"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator( IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }
                    var role = new IdentityRole { Name = AdministratorRoleName };
                    await roleManager.CreateAsync(role);

                    const string email = "admin@admin.com";
                    const string password = "admin123";

                    var user = new User
                    {
                        Email = email,
                        UserName = email
                    };

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, role.Name);

                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
