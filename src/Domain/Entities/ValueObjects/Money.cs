
using App.Domain.Shared;

namespace App.Domain.Entities;

public class Money: ValueObject
{
    public string Currency { get;  }
    public decimal Amount { get; }

    protected Money()
    {
    }

    private Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public static Either<IError, Money> Create( string currency, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return Left(Error.Create("Currency required", nameof(Currency)));
        if (currency.Length > 3)
            return Left(Error.Create("Currency is too long", nameof(Currency)));

        if (amount < 0)
            return Left(Error.Create("Amount must be positive", nameof(Amount)));

        return new Money(currency, amount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
}