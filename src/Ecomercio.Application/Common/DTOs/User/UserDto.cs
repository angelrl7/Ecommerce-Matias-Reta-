namespace Ecomercio.Application.Common.DTOs.User;

public record UserDto(
    Guid Id,
    string Email,
    string FullName,
    string Role,
    DateTime CreatedAt
);