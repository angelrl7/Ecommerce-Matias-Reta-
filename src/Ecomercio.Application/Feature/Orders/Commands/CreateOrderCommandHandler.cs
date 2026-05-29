using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Order;

namespace Ecomercio.Application.Feature.Orders.Commands;

public class CreateOrderCommandHandler(IOrderRepository orderRepository)
    : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var items = request.Items
            .Select(i => OrderItem.Create(i.ProductId, i.Quantity, i.UnitPrice))
            .ToList();

        var order = Order.Create(request.UserId, items);
        await orderRepository.AddAsync(order, ct);

        return ToDto(order);
    }

    private static OrderDto ToDto(Order order) => new(
        order.Id,
        order.UserId,
        order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice)).ToList(),
        order.TotalAmount,
        order.Status,
        order.CreatedAt,
        order.UpdatedAt
    );
}
