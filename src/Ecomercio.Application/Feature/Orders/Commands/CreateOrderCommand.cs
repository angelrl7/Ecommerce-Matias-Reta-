using MediatR;
using Ecomercio.Application.Common.DTOs.Order;

namespace Ecomercio.Application.Feature.Orders.Commands;

public record CreateOrderCommand(Guid UserId, List<OrderItemDto> Items) : IRequest<OrderDto>;
