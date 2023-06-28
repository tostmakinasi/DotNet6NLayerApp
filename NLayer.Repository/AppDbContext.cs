using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //var p = new Product() { ProductFeature = new ProductFeature() };
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature { Id = 1, Color = "Kırmızı", Width = 100, Height = 100, ProductId = 1 });
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity entityReferance)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entityReferance.IsDeleted = true;
                            entityReferance.UpdatedDate = DateTime.Now;
                            Entry(entityReferance).Property(x => x.CreatedDate).IsModified = false;
                            break;
                        case EntityState.Modified:
                            entityReferance.UpdatedDate = DateTime.Now;
                            Entry(entityReferance).Property(x => x.CreatedDate).IsModified = false;
                            break;
                        case EntityState.Added:
                            entityReferance.CreatedDate = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
