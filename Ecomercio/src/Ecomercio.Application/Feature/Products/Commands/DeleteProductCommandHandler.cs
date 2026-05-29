using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Domain.Interfaces;

namespace Ecomercio.Application.Feature.Products.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(request.Id, ct);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        await _repository.DeleteAsync(product, ct);
    }
}
