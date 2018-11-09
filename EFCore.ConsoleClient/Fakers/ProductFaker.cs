using Bogus;
using EFCore.Models;

namespace EFCore.ConsoleClient.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Commerce.Product());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.Barcode, f => f.Commerce.Ean13());
            Ignore(p => p.ProductTags);
        }
    }
}
