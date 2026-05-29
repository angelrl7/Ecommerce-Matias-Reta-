using MediatR;
using Ecomercio.Application.Common.DTOs.Product;

namespace Ecomercio.Application.Feature.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;