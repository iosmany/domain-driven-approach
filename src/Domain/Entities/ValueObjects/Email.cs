
namespace App.Domain.Entities;

using System.Text.RegularExpressions;
using Domain.Shared;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Either<IError, Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Left(Error.Create("Email required", nameof(Email)));

        email = email.Trim();

        if (email.Length > 200)
            return Left(Error.Create("Exceed allowed lenght", nameof(Email)));

        if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
            return Left(Error.Create("Email is invalid", nameof(Email)));

        return new Email(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }
}