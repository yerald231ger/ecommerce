namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; }
    public string LastName { get; }
    public string EmailAddress { get; }
    public string AddressLine { get; }
    public string Country { get; }
    public string State { get; }
    public string ZipCode { get; }

    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country,
        string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country,
        string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new DomainException($"{nameof(emailAddress)} is required.");

        if (string.IsNullOrWhiteSpace(addressLine))
            throw new DomainException($"{nameof(addressLine)} is required.");

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
    }
}