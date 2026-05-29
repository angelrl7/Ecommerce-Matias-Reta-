namespace Ecomercio.Application.Common.DTOs.Product;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId
);