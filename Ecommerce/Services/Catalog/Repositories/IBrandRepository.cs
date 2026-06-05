using Catalog.Entities;
using Catalog.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetBrandsAsync();
        Task<ProductBrand> GetBrandByIdAsync(string id);
        Task CreateBrandAsync(ProductBrand brand);
        Task UpdateBrandAsync(string id, ProductBrand brand);
        Task DeleteBrandAsync(string id);
    }

    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brandCollection;
        public BrandRepository(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            _brandCollection = new MongoClient(catalogDatabaseSettings.Value.ConnectionString)
            .GetDatabase(catalogDatabaseSettings.Value.DatabaseName)
            .GetCollection<ProductBrand>(catalogDatabaseSettings.Value.BrandCollectionName);
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return await _brandCollection.Find(_ => true).ToListAsync();
        }
        public async Task<ProductBrand> GetBrandByIdAsync(string id)
        {
            return await _brandCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateBrandAsync(ProductBrand brand)
        {
            await _brandCollection.InsertOneAsync(brand);
        }
        public async Task UpdateBrandAsync(string id, ProductBrand brand)
        {
            await _brandCollection.ReplaceOneAsync(x => x.Id == id, brand);
        }
        public async Task DeleteBrandAsync(string id)
        {
            await _brandCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
