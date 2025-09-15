using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "iPhone X",
                    Summary = "This phone is the company's biggest change to its flagship",
                    Description = "Lorem ipsum dolor sit amet.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung Galaxy S21",
                    Summary = "Next-gen phone with powerful camera",
                    Description = "High resolution display and sleek design.",
                    ImageFile = "product-2.png",
                    Price = 850.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Google Pixel 7",
                    Summary = "Clean Android experience with strong AI features",
                    Description = "Stock Android with powerful performance.",
                    ImageFile = "product-3.png",
                    Price = 799.99M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Dell XPS 13",
                    Summary = "Lightweight ultrabook with amazing display",
                    Description = "13-inch 4K display, Intel i7 processor.",
                    ImageFile = "product-4.png",
                    Price = 1199.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "Sony WH-1000XM4",
                    Summary = "Top-tier noise-cancelling headphones",
                    Description = "Perfect for travel and remote work.",
                    ImageFile = "product-5.png",
                    Price = 349.99M,
                    Category = "Audio"
                }
            };
        }
    }
}
