
namespace App.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using App.Domain.Shared;
using LanguageExt.UnsafeValueAccess;

public class OrderLine : Audit, ISoftDelete
{
    public virtual Product Product { get; private set; }
    public int Quantity { get; private set; }
    public virtual ProductPrice Price { get; private set; }
    public Money Total { get; private set; }
    public bool IsDeleted { get; private set; }

    [ConcurrencyCheck]
    public byte[] RowVersion { get; private set; }

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