using Microsoft.EntityFrameworkCore;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Infrastructure.Persistence;

namespace Ecomercio.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) => _context = context;

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Products.FindAsync(new object[] { id }, cancellationToken: ct);

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        => await _context.Products.AsNoTracking().ToListAsync(ct);

    public async Task<Product?> GetByNameAsync(string name, CancellationToken ct = default)
        => await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name, ct);

    public async Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken ct = default)
        => await _context.Products.AsNoTracking().Where(p => p.IsActive).ToListAsync(ct);

    public async Task<bool> ExistsAsync(string name, CancellationToken ct = default)
        => await _context.Products.AnyAsync(p => p.Name == name, ct);

    public async Task AddAsync(Product entity, CancellationToken ct = default)
    {
        await _context.Products.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Product entity, CancellationToken ct = default)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Product entity, CancellationToken ct = default)
    {
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}