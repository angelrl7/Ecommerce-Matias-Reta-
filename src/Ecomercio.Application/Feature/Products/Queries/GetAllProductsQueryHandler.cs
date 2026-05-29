using MediatR;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Product;

namespace Ecomercio.Application.Feature.Products.Queries;

public class GetAllProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
    {
        var products = await repository.GetAllAsync(ct);
        return products
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.IsActive, p.CategoryId))
            .ToList();
    }
}
