using Catalog.Entities;

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
}
