

using App.Domain.Shared;

namespace App.Domain.Entities;

public class ProductPrice : ValueObject
{
    public decimal Price { get; private set; }
    public decimal DiscountPercentage { get; private set; }   
    public decimal NetPrice => Math.Round(Price - (Price * (DiscountPercentage /100)), 2);

    protected ProductPrice()
    {
    }

    private ProductPrice(decimal price, decimal discount) : this()
    {
        Price = price;
        DiscountPercentage = discount;
    } 

    public static Either<IError, ProductPrice> Create(decimal price, decimal discount)
    {
        if (price < 0)
            return Left(Error.Create("Price cannot be negative", nameof(price)));
        if (discount < 0 || discount > 100)
            return Left(Error.Create("Discount must be between 0 and 100", nameof(discount)));
        return new ProductPrice(price, discount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return DiscountPercentage;
    }
}