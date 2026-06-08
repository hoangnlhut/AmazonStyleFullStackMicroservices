using Catalog.Entities;
using Catalog.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Data
{
    public class DatabaseSeeder
    {
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<ProductBrand> _brandsCollection;
        private readonly IMongoCollection<ProductType> _typesCollection;
        public DatabaseSeeder(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            var client = new MongoClient(catalogDatabaseSettings.Value.ConnectionString);
            var db = client.GetDatabase(catalogDatabaseSettings.Value.DatabaseName);
            _productsCollection = db.GetCollection<Product>(catalogDatabaseSettings.Value.ProductCollectionName);
            _brandsCollection = db.GetCollection<ProductBrand>(catalogDatabaseSettings.Value.BrandCollectionName);
            _typesCollection = db.GetCollection<ProductType>(catalogDatabaseSettings.Value.TypeCollectionName);
        }
        public async Task SeedAsync()
        {
            var seedNasePath = Path.Combine("Data", "SeedData");
            // 1. Check if the collection already contains data

            #region check brands data
            var hasBrandData = await _brandsCollection.Find(_ => true).AnyAsync();
            if (!hasBrandData)
            {
                // 2. Define initial seed data
                var brandData = await File.ReadAllTextAsync(Path.Combine(seedNasePath, "brands.json"));
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData) ?? throw new Exception("Brand data is invalid");

                // 3. Batch insert documents
                await _brandsCollection.InsertManyAsync(brands);
            }
            #endregion

            #region check types data
            var hasTypeData = await _typesCollection.Find(_ => true).AnyAsync();
            if (!hasTypeData)
            {
                // 2. Define initial seed data
                var typeData = await File.ReadAllTextAsync(Path.Combine(seedNasePath, "types.json"));
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData) ?? throw new Exception("Type data is invalid");

                // 3. Batch insert documents
                await _typesCollection.InsertManyAsync(types);
            }
            #endregion

            #region check products data
            var hasProductData = await _productsCollection.Find(_ => true).AnyAsync();
            if (!hasProductData)
            {
                // 2. Define initial seed data
                var productData = await File.ReadAllTextAsync(Path.Combine(seedNasePath, "products.json"));
                var products = JsonSerializer.Deserialize<List<Product>>(productData) ?? throw new Exception("Product data is invalid");

                foreach (var product in products)
                {
                    //reset Id to let MongoDB generate a new one
                    product.Id = null;

                    // Set the CreatedDate to the current date and time
                    product.CreatedDate = DateTimeOffset.UtcNow;
                }

                // 3. Batch insert documents
                await _productsCollection.InsertManyAsync(products);
            }
            #endregion
        }
    }
}
