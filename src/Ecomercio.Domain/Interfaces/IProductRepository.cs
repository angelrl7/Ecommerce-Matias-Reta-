using Ecomercio.Domain.Entities;

namespace Ecomercio.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken ct = default);
    Task<bool> ExistsAsync(string name, CancellationToken ct = default);
}