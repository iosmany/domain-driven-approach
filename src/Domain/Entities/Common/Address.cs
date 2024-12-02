

using App.Domain.Shared;

namespace App.Domain.Entities;

public class Address: ValueObject
{
    public string Street { get;  }
    public string? City { get; }
    public string? State { get; }
    public string Country { get; }
    public string ZipCode { get; }
    public string? Phone { get; }

    protected Address()
    {
        Street = string.Empty;
        Country = string.Empty;
        ZipCode = string.Empty;
    }

    private Address(string street, string? city, string? state, string country, string zipCode, string? phone)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Phone = phone;
    }

    public static Either<IError, Address> Create(string street, string? city, string? state, string country, string zipCode, string? phone)
    {
        if (string.IsNullOrWhiteSpace(street))
            return Left(Error.Create("Street required", nameof(Street)));
        if (street.Length > 200)
            return Left(Error.Create("Street is too long", nameof(Street)));

        if (string.IsNullOrWhiteSpace(country))
            return Left(Error.Create("Country required", nameof(Country)));
        if (country.Length > 3)
            return Left(Error.Create("Country is too long", nameof(Country)));

        if (string.IsNullOrWhiteSpace(zipCode))
            return Left(Error.Create("ZipCode required", nameof(ZipCode)));
        if (zipCode.Length > 10)
            return Left(Error.Create("ZipCode is too long", nameof(ZipCode)));

        if (!string.IsNullOrWhiteSpace(phone) && phone.Length > 20)
            return Left(Error.Create("Phone is too long", nameof(Phone)));
        
        if (!string.IsNullOrWhiteSpace(city) && city.Length > 200)
            return Left(Error.Create("City is too long", nameof(City)));

        return new Address(street, city, state, country, zipCode, phone);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return ZipCode;
        yield return Country;
    }
}