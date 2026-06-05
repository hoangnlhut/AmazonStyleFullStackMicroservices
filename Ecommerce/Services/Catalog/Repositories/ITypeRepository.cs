using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetTypesAsync();
        Task<ProductType> GetTypeByIdAsync(string id);
        Task CreateTypeAsync(ProductType type);
        Task UpdateTypeAsync(string id, ProductType type);
        Task DeleteTypeAsync(string id);
    }
}
