using Microsoft.EntityFrameworkCore;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Infrastructure.Persistence;

namespace Ecomercio.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Users.FindAsync(new object[] { id }, cancellationToken: ct);

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
        => await context.Users.AsNoTracking().ToListAsync(ct);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email.Trim().ToLowerInvariant(), ct);

    public async Task<bool> ExistsAsync(string email, CancellationToken ct = default)
        => await context.Users.AnyAsync(u => u.Email == email.Trim().ToLowerInvariant(), ct);

    public async Task AddAsync(User entity, CancellationToken ct = default)
    {
        await context.Users.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(User entity, CancellationToken ct = default)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(User entity, CancellationToken ct = default)
    {
        context.Users.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}
