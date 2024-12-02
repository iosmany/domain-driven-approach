
using App.Domain.Shared;

namespace App.Domain.Entities;

public class Money: ValueObject
{
    public Currency Currency { get;  }
    public decimal Amount { get; }

    protected Money()
    {
        Currency = Currency.USD;
        Amount = 0;
    }

    private Money(Currency currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public static Either<IError, Money> Create(Currency currency, decimal amount)
    {
        return new Money(currency, amount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
}