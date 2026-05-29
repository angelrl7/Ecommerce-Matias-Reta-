namespace Ecomercio.Application.Common.DTOs.Product;

public record UpdateProductDto(
    string? Name,
    string? Description,
    decimal? Price,
    int? Stock,
    Guid? CategoryId
);