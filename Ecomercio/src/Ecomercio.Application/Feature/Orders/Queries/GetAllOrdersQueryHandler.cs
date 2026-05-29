using MediatR;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Order;

namespace Ecomercio.Application.Feature.Orders.Queries;

public class GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersQuery, IReadOnlyList<OrderDto>>
{
    public async Task<IReadOnlyList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken ct)
    {
        var orders = await orderRepository.GetAllAsync(ct);
        return orders.Select(order => new OrderDto(
            order.Id,
            order.UserId,
            order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice)).ToList(),
            order.TotalAmount,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt
        )).ToList().AsReadOnly();
    }
}
