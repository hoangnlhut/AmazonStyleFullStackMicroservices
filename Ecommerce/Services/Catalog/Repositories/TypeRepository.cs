using Catalog.Entities;
using Catalog.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<ProductType> _typeCollection;
        public TypeRepository(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            _typeCollection = new MongoClient(catalogDatabaseSettings.Value.ConnectionString)
            .GetDatabase(catalogDatabaseSettings.Value.DatabaseName)
            .GetCollection<ProductType>(catalogDatabaseSettings.Value.TypeCollectionName);
        }
        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            return await _typeCollection.Find(_ => true).ToListAsync();
        }
        public async Task<ProductType> GetTypeByIdAsync(string id)
        {
            return await _typeCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateTypeAsync(ProductType type)
        {
            await _typeCollection.InsertOneAsync(type);
        }
        public async Task UpdateTypeAsync(string id, ProductType type)
        {
            await _typeCollection.ReplaceOneAsync(t => t.Id == id, type);
        }
        public async Task DeleteTypeAsync(string id)
        {
            await _typeCollection.DeleteOneAsync(t => t.Id == id);
        }
    }
}
