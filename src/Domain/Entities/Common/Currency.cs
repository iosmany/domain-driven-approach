

namespace App.Domain.Entities;

using System.Collections.Generic;
using App.Domain.Shared;
public class Currency : ValueObject
{
    public string Code { get; }
    public string Symbol { get; }
    public int DecimalPlaces { get; }


    public static Currency USD => new Currency("USD", "$", 2);
    public static Currency EUR => new Currency("EUR", "€", 2);

    protected Currency()
    {
    }

    private Currency(string code, string symbol, int decimalPlaces)
    {
        Code = code;
        Symbol = symbol;
        DecimalPlaces = decimalPlaces;
    }

    public static Either<IError, Currency> Create(string code, string symbol, int decimalPlaces)
    {   
        if (string.IsNullOrWhiteSpace(code))
            return Left(Error.Create("Currency code is required", nameof(Code)));
        if (string.IsNullOrWhiteSpace(symbol))
            return Left(Error.Create("Currency symbol is required", nameof(Symbol)));
        if (decimalPlaces < 0)
            return Left(Error.Create("Currency decimal places must be greater than or equal to zero", nameof(DecimalPlaces)));

        if(code.Length != 3)
            return Left(Error.Create("Currency code must be 3 characters long", nameof(Code)));
        if(symbol.Length != 1)
            return Left(Error.Create("Currency symbol must be 1 character long", nameof(Symbol)));

        return new Currency(code, symbol, decimalPlaces);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Symbol;
        yield return DecimalPlaces;
    }
}