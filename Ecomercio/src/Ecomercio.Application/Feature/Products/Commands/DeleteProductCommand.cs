using MediatR;

namespace Ecomercio.Application.Feature.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest;
