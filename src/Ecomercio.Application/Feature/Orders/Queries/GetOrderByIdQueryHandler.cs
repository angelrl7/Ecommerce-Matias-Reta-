using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Application.Common.DTOs.Order;

namespace Ecomercio.Application.Feature.Orders.Queries;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        var order = await orderRepository.GetByIdAsync(request.Id, ct);
        if (order is null)
            throw new NotFoundException(nameof(Order), request.Id);

        return new OrderDto(
            order.Id,
            order.UserId,
            order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice)).ToList(),
            order.TotalAmount,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt
        );
    }
}
