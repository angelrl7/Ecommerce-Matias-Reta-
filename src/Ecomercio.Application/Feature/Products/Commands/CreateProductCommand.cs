using MediatR;
using Ecomercio.Application.Common.DTOs.Product;

namespace Ecomercio.Application.Feature.Products.Commands;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId

) : IRequest<ProductDto>;