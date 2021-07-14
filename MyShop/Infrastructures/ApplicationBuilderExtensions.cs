namespace MyShop.Infrastructures
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyShop.Data;
    using MyShop.Data.Models;
    using System;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServces = app.ApplicationServices.CreateScope();
            var data = scopedServces.ServiceProvider.GetService<MyShopDbContext>();
            data.Database.Migrate();

            SeedCategories(data);
            SeedTowns(data);
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
                new Category{ Name = "Rent"},
                new Category{ Name = "Fashion"},
                new Category{ Name = "Autos"},                
                new Category{ Name = "Services"},
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
    }
}
