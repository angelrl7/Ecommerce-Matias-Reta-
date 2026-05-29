using Microsoft.EntityFrameworkCore;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Infrastructure.Persistence;

namespace Ecomercio.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken ct = default)
        => await context.Orders.Include(o => o.Items).AsNoTracking().ToListAsync(ct);

    public async Task<IReadOnlyList<Order>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        => await context.Orders.Include(o => o.Items).AsNoTracking()
            .Where(o => o.UserId == userId).ToListAsync(ct);

    public async Task AddAsync(Order entity, CancellationToken ct = default)
    {
        await context.Orders.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Order entity, CancellationToken ct = default)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Order entity, CancellationToken ct = default)
    {
        context.Orders.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}
