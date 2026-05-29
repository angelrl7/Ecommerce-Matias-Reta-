using Ecomercio.Domain.Exceptions;

namespace Ecomercio.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Order() { }

    public static Order Create(Guid userId, List<OrderItem> items)
    {
        if (userId == Guid.Empty)
            throw new DomainException("El usuario es obligatorio.");
        if (items == null || items.Count == 0)
            throw new DomainException("La orden debe tener al menos un ítem.");

        var order = new Order
        {
            UserId = userId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            Items = items,
            TotalAmount = items.Sum(i => i.UnitPrice * i.Quantity)
        };

        return order;
    }
}
