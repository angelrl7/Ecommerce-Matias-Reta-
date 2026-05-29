namespace Ecomercio.Application.Common.DTOs.Product;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    bool IsActive,
    Guid CategoryId
);