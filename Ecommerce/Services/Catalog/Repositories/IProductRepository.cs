using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> UpdateProductAsync(Product product);

    }
}
