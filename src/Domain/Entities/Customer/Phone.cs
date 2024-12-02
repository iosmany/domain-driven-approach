
namespace App.Domain.Entities;

using System.Text.RegularExpressions;
using Domain.Shared;

public class Phone : ValueObject
{
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }

    public static Either<IError, Phone> Create(string Phone)
    {
        if (string.IsNullOrWhiteSpace(Phone))
            return Left(Error.Create("Phone required", nameof(Phone)));

        Phone = Phone.Trim();
        if (Phone.Length > 20)
            return Left(Error.Create("Exceed allowed lenght", nameof(Phone)));
        if (!Regex.IsMatch(Phone, @"^\+?[1-9]\d{1,14}$"))
            return Left(Error.Create("Phone is invalid", nameof(Phone)));

        return new Phone(Phone);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Phone Phone)
    {
        return Phone.Value;
    }
}