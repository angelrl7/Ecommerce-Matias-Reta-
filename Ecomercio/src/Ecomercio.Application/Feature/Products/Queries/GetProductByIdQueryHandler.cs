using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Product;

namespace Ecomercio.Application.Feature.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(request.Id, ct);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.IsActive,
            product.CategoryId
        );
    }
}