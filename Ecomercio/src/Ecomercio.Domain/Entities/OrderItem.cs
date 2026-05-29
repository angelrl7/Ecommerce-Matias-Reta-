using Ecomercio.Domain.Exceptions;

namespace Ecomercio.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    private OrderItem() { }

    public static OrderItem Create(Guid productId, int quantity, decimal unitPrice)
    {
        if (productId == Guid.Empty)
            throw new DomainException("El producto es obligatorio.");
        if (quantity <= 0)
            throw new DomainException("La cantidad debe ser mayor a cero.");
        if (unitPrice <= 0)
            throw new DomainException("El precio unitario debe ser mayor a cero.");

        return new OrderItem
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
}
