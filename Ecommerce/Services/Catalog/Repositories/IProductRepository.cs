using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> UpdateProductAsync(Product product);

    }
}
