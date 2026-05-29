using MediatR;
using Ecomercio.Application.Common.DTOs.Order;

namespace Ecomercio.Application.Feature.Orders.Queries;

public record GetAllOrdersQuery() : IRequest<IReadOnlyList<OrderDto>>;
