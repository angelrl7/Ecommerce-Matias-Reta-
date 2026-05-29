namespace Ecomercio.Application.Common.DTOs.Order;


public record OrderDto(
    Guid Id,
    Guid UserId,
    List<OrderItemDto> Items,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);