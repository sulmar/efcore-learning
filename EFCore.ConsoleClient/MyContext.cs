using EFCore.ConsoleClient.Configurations;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ConsoleClient
{
    // dotnet add package Microsoft.EntityFrameworkCore
    public class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public MyContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite("Filename=./eshop.sqlite");
            // optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=eshopdb;Integrated Security=true;MultipleActiveResultSets=true");

            // add package Microsoft.EntityFrameworkCore.Sqlite
            optionsBuilder.UseSqlite("Filename=./eshop.sqlite");

            //   optionsBuilder.UseInMemoryDatabase("Test");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customizations must go after base.OnModelCreating(builder)

            // modelBuilder.Entity<Order>()
            //     .HasOne<Customer>()
            //     .WithMany(c=>c.Orders)
            //     .HasForeignKey(p=>p.CustomerId);

            // alternatywnie mozna wyjsc od drugiej encji

            // modelBuilder.Entity<Customer>()
            //     .HasMany(c=>c.Orders)
            //     .WithOne(o=>o.Customer)
            //     .HasForeignKey(o=>o.CustomerId)
            //     .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            // 1-1
            modelBuilder.Entity<Order>()
                .HasOne<Payment>()
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.PaymentId);

            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new {
                    pt.ProductId,
                    pt.TagId
                });

            modelBuilder.Entity<ProductTag>()
                .HasOne<Product>(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductTag>()
                .HasOne<Tag>(pt => pt.Tag)
                .WithMany(t => t.ProducTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<Product>().HasBaseType<Item>();
            modelBuilder.Entity<Service>().HasBaseType<Item>();

            modelBuilder.Entity<Item>().ToTable("Items");

            //modelBuilder.Query<CustomerTotalAmount>().ToView("vwCustomerTotalAmounts");





        }

    }
}
