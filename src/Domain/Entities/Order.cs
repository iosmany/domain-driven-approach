


using System.ComponentModel.DataAnnotations;
using App.Domain.Shared;
using LanguageExt.UnsafeValueAccess;

namespace App.Domain.Entities;
public class Order : Entity, IAudit, ISoftDelete
{
    public virtual User User { get; private set; }
    public virtual Address InvoiceAddress { get; private set; }
    public virtual Address ShippingAddress { get; private set; }
    public int Quantity { get; private set; }
    public Money Total { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    protected Order()
    {
    }

    public Order(User user, int quantity, Money total) : this()
    {
        User = user;
        Quantity = quantity;

        Address.Create(user.InvoiceAddress.Street,
                    user.InvoiceAddress.City,
                    user.InvoiceAddress.State,
                    user.InvoiceAddress.ZipCode,
                    user.InvoiceAddress.Country, user.InvoiceAddress.Phone
        ).Match(
            Right: address => InvoiceAddress = address,
            Left: ex => throw new Exception(ex.Message)
        );

        var shippingAddress = user.ShippingAddress ?? user.InvoiceAddress;
        Address.Create(shippingAddress.Street,
                    shippingAddress.City,
                    shippingAddress.State,
                    shippingAddress.ZipCode,
                    shippingAddress.Country, shippingAddress.Phone
        ).Match(
            Right: address => ShippingAddress = address,
            Left: ex => throw new Exception(ex.Message)
        );

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Total = total;

    }
}