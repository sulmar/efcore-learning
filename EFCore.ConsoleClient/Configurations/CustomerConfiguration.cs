using EFCore.ConsoleClient.Extensions;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ConsoleClient.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Owned Entity Type
            //   builder.OwnsOne(p=>p.ShippingAddress);

          

            // KONWERTERY
            
            // konwersja
            // builder.Property(p=>p.Gender)
            //     .HasConversion(
            //         v => v.ToString(),
            //         v => (Gender)Enum.Parse(typeof(Gender), v)
            //     );

            // Klasa konwertera
            // var converter = new ValueConverter<Gender, string>(
            //     v => v.ToString(),
            //     v => (Gender)Enum.Parse(typeof(Gender), v));

            // wbudowany konwerter
            // https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions
            // var converter = new EnumToStringConverter<Gender>();

            //  builder.Property(p=>p.Gender)
            //     .HasConversion(converter);

            // predefiniowane konwersje
            builder.Property(p=>p.Gender)
                .HasConversion<string>();

            // Użycie wbudowanego konwertera
            builder.Property(p=>p.IsDeleted)
                .HasConversion(new BoolToStringConverter("Y", "N"));

            // Konwerter za pomocą wyrażenia lambda
            //builder.Property(p => p.ShippingAddress).HasConversion(

            //    v => JsonConvert.SerializeObject(v),
            //    v => JsonConvert.DeserializeObject<Address>(v));

            // Użycie własnego konwertera
            // builder.Property(p => p.ShippingAddress).HasConversion(new JsonValueConverter<Address>());

            // Użycie własnego konwertera za pomocą metody rozszerzającej
            builder.Property(p => p.ShippingAddress)
                .HasJsonValueConversion();

            // builder.Property(p=>p.Gender)
            //     .HasColumnType("nvarchar(20)");


            builder.HasData(
                new Customer { Id = 1, FirstName = "John", LastName = "Smith" },
                new Customer { Id = 2, FirstName = "Ann", LastName = "Smith" }
                );

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
