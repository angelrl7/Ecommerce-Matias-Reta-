namespace Ecomercio.Application.Common.DTOs.Order;


public record CreateOrderDto(
    Guid UserId,
    List<OrderItemDto> Items
);