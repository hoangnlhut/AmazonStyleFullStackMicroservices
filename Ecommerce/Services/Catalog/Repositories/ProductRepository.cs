using Catalog.Entities;
using Catalog.Models;
using Catalog.Specifications;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<ProductBrand> _brandsCollection;
        private readonly IMongoCollection<ProductType> _typesCollection;
        public ProductRepository(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            var client = new MongoClient(catalogDatabaseSettings.Value.ConnectionString);
            var db = client.GetDatabase(catalogDatabaseSettings.Value.DatabaseName);
            _productCollection = db.GetCollection<Product>(catalogDatabaseSettings.Value.ProductCollectionName);
            _brandsCollection = db.GetCollection<ProductBrand>(catalogDatabaseSettings.Value.BrandCollectionName);
            _typesCollection = db.GetCollection<ProductType>(catalogDatabaseSettings.Value.TypeCollectionName);

        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams)
        {
            // 1. Initialize an empty filter to get all records, or insert custom matching conditions
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty; // Start with an empty filter that matches all documents

            if (!string.IsNullOrEmpty(specParams.Search))
            {
                filter &= builder.Regex(p => p.Name, new BsonRegularExpression($".*{specParams.Search}.*", "i"));
            }

            if (!string.IsNullOrEmpty(specParams.TypeId))
            {
                filter &= builder.Eq(p => p.Type.Id, specParams.TypeId);
            }

            if (!string.IsNullOrEmpty(specParams.BrandId))
            {
                filter &= builder.Eq(p => p.Brand.Id, specParams.BrandId);
            }

            // 2. Fire total document count estimation concurrently or sequentially
            var count = await _productCollection.CountDocumentsAsync(filter);
            var data = await ApplyDataFilter(specParams, filter);

            return new Pagination<Product>(specParams.PageIndex, specParams.PageSize, count, data);

        }

        private async Task<IReadOnlyCollection<Product>> ApplyDataFilter(CatalogSpecParams specParams, FilterDefinition<Product> filter)
        {
            var sortBuilder = Builders<Product>.Sort.Ascending(p => p.Name);
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                sortBuilder = specParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name)
                };
            }

            // 3. Apply pagination arithmetic directly into the Mongo query string
            return await _productCollection.Find(filter).Sort(sortBuilder)
                .Skip((specParams.PageIndex - 1) * specParams.PageSize)
                .Limit(specParams.PageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand)
        {
            return await _productCollection.Find(p => p.Brand.Name.ToLower().Trim() == brand.ToLower().Trim()).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}.*", "i"));
            return await _productCollection.Find(filter).ToListAsync();
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            var result = await _productCollection.ReplaceOneAsync(p => p.Id == product.Id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<ProductType> GetTypeByIdAsync(string typeId)
        {
            return await _typesCollection.Find(t => t.Id == typeId).FirstOrDefaultAsync();
        }

        public async Task<ProductBrand> GetBrandByIdAsync(string brandId)
        {
            return await _brandsCollection.Find(b => b.Id == brandId).FirstOrDefaultAsync();
        }
    }
}
