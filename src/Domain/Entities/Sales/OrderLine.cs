
namespace App.Domain.Entities;

using App.Domain.Shared;

public class OrderLine : AuditAggregateRoot, ISoftDelete
{
    public virtual Product Product { get; private set; }
    public int Quantity { get; private set; }
    public virtual PriceAmount Price { get; private set; }
    public Money Total { get; private set; }
    public bool IsDeleted { get; private set; }

    protected OrderLine()
    {
    }

    public OrderLine(Product product, int quantity) : this()
    {
        Product = product;
        Quantity = quantity;
        
        Money.Create(Currency.USD, 0)
            .BiBind<Money>(
                total => {
                    Total = total;
                    return total;
                },
                (err) => throw new InvalidOperationException("Invalid currency")
            );
    }
}