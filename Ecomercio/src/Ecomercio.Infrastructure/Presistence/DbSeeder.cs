using System.Security.Cryptography;
using System.Text;
using Ecomercio.Domain.Constants;
using Ecomercio.Domain.Entities;
using Ecomercio.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecomercio.Infrastructure.Presistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, IConfiguration configuration)
    {
        if (await context.Users.AnyAsync(u => u.Role == UserRoles.Admin))
            return;

        var email = configuration["Seed:AdminEmail"] ?? "admin@ecommerce.com";
        var password = configuration["Seed:AdminPassword"] ?? "Admin123!";
        var name = configuration["Seed:AdminName"] ?? "Administrador";

        var admin = User.Create(email, ComputeHash(password), name, UserRoles.Admin);
        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();
    }

    private static string ComputeHash(string password)
        => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
