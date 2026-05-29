using Ecomercio.Domain.Constants;
using Ecomercio.Domain.Exceptions;

namespace Ecomercio.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User Create(string email, string passwordHash, string name, string role = UserRoles.Customer)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email es obligatorio.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("La contraseña es obligatoria.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre es obligatorio.");

        return new User
        {
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            Name = name.Trim(),
            Role = role,
            CreatedAt = DateTime.UtcNow
        };
    }
}
