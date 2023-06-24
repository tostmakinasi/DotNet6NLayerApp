using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Kitap 1", Stock = 50, Price = 89.9m, CategoryId = 1 },
                new Product { Id = 2, Name = "Kitap 2", Stock = 60, Price = 69.9m, CategoryId = 1 },
                new Product { Id = 3, Name = "Kalem 1", Stock = 150, Price = 9.9m, CategoryId = 2 },
                new Product { Id = 4, Name = "Kalem 2", Stock = 250, Price = 19.9m, CategoryId = 2 },
                new Product { Id = 5, Name = "Defter 1", Stock = 50, Price = 29.9m, CategoryId = 3 },
                new Product { Id = 6, Name = "Defter 2", Stock = 70, Price = 9.9m, CategoryId = 3 },
                new Product { Id = 7, Name = "Kalem Traş 1", Stock = 250, Price = 5.9m, CategoryId = 4 },
                new Product { Id = 8, Name = "Kalem Traş 2", Stock = 350, Price = 5.9m, CategoryId = 4 });
        }
    }
}
