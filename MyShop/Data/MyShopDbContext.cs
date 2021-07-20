﻿namespace MyShop.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyShop.Data.Models;

    public class MyShopDbContext : IdentityDbContext
    {
        public MyShopDbContext(DbContextOptions<MyShopDbContext> options)
            : base(options)
        {
        }
        public DbSet<Goods> Goods { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Chat> Chats { get; init; }
        public DbSet<Comment> Comments { get; init; }
        public DbSet<Purchase> Purchases { get; init; }
        public DbSet<Town> Towns { get; init; }
        public DbSet<Merchant> Merchants { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>()
              .HasOne<IdentityUser>()
              .WithOne()
              .HasForeignKey<Merchant>(c => c.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Goods)
                .WithMany(g => g.Chats)
                .HasForeignKey(c => c.GoodsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Chat)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(c => c.Goods)
                .WithMany(c => c.Purchases)
                .HasForeignKey(c => c.GoodsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Goods>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Goods)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Goods>()
                .HasOne(c => c.Town)
                .WithMany(c => c.Goods)
                .HasForeignKey(c => c.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Goods>()
                .HasOne(c => c.Merchant)
                .WithMany(c => c.Goods)
                .HasForeignKey(c => c.MerchantId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
