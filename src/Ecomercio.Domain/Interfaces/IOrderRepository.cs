using Ecomercio.Domain.Entities;

namespace Ecomercio.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IReadOnlyList<Order>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
}
