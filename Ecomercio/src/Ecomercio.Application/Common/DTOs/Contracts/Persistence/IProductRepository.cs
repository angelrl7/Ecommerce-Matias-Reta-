using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;

namespace Ecomercio.Application.Contracts.Persistence;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken ct = default);
}