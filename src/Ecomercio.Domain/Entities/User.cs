using Ecomercio.Domain.Exceptions;

namespace Ecomercio.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Customer";
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User Create(string email, string passwordHash, string fullName, string role = "Customer")
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email es obligatorio.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("La contraseña es obligatoria.");
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("El nombre completo es obligatorio.");
        if (string.IsNullOrWhiteSpace(role))
            throw new DomainException("El rol es obligatorio.");

        // Validar formato de email
        if (!email.Contains("@"))
            throw new DomainException("El email no tiene un formato válido.");

        return new User
        {
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            FullName = fullName.Trim(),
            Role = role.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}