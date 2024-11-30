
namespace App.Domain.Entities;

using LanguageExt.UnsafeValueAccess;

public class OrderLine 
{
    public virtual Product Product { get; private set; }
    public int Quantity { get; private set; }
    public virtual ProductPrice Price { get; private set; }
    public Money Total { get; private set; }

    protected OrderLine()
    {
    }

    public OrderLine(Product product, int quantity) : this()
    {
        Product = product;
        Quantity = quantity;
        Money.Create("USD", 0)
            .BiBind<Money>(
                total => {
                    Total = total;
                    return total;
                },
                (err) => throw new InvalidOperationException("Invalid currency")
            );
    }
}