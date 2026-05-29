namespace Ecomercio.Application.Common.DTOs.Order;


public record OrderItemDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);