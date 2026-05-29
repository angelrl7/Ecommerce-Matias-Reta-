using Microsoft.EntityFrameworkCore;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Infrastructure.Persistence;

namespace Ecomercio.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Categories.FindAsync(new object[] { id }, cancellationToken: ct);

    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default)
        => await context.Categories.AsNoTracking().ToListAsync(ct);

    public async Task<Category?> GetByNameAsync(string name, CancellationToken ct = default)
        => await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name, ct);

    public async Task<bool> ExistsAsync(string name, CancellationToken ct = default)
        => await context.Categories.AnyAsync(c => c.Name == name, ct);

    public async Task AddAsync(Category entity, CancellationToken ct = default)
    {
        await context.Categories.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Category entity, CancellationToken ct = default)
    {
        context.Categories.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Category entity, CancellationToken ct = default)
    {
        context.Categories.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}
