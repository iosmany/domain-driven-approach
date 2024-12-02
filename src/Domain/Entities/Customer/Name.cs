
namespace App.Domain.Entities;

using App.Domain.Shared;

public class Name : ValueObject
{
    public string FirstName { get; }
    public string? LastName { get; }
    public virtual Suffix Suffix { get; }

    protected Name()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Suffix = Suffix.None;
    }

    private Name(string firstName, string? lastName, Suffix suffix)
        : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Suffix = suffix;
    }

    public static Either<IError, Name> Create(string firstName, string? lastName, Suffix suffix)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Left(Error.Create("First name should not be empty", nameof(FirstName)));

        firstName = firstName.Trim();
        lastName = lastName?.Trim();

        if (firstName.Length > 80)
            return Left(Error.Create("First name is too long", nameof(FirstName)));
        if (!string.IsNullOrWhiteSpace(lastName) && lastName?.Length > 100)
            return Left(Error.Create("Last name is too long", nameof(LastName)));    

        return new Name(firstName, lastName, suffix);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName ?? string.Empty;
        yield return Suffix;
    }
}