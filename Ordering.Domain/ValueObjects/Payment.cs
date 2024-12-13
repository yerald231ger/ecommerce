namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardHolderName { get; }
    public string CardNumber { get; }
    public string CardExpiration { get; }
    public string CardCvv { get; }
    public int PaymentMethod { get; }
    
    private Payment(string? cardHolderName, string cardNumber, string cardExpiration, string cardCvv, int paymentMethod)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        CardExpiration = cardExpiration;
        CardCvv = cardCvv;
        PaymentMethod = paymentMethod;
    }
    
    public static Payment Of(string? cardHolderName, string cardNumber, string cardExpiration, string cardCvv, int paymentMethod)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            throw new DomainException($"{nameof(cardNumber)} is required.");

        if (string.IsNullOrWhiteSpace(cardExpiration))
            throw new DomainException($"{nameof(cardExpiration)} is required.");

        if (string.IsNullOrWhiteSpace(cardCvv))
            throw new DomainException($"{nameof(cardCvv)} is required.");
        
        if (cardCvv.Length != 3)
            throw new DomainException($"{nameof(cardCvv)} must be 3 digits.");

        return new Payment(cardHolderName, cardNumber, cardExpiration, cardCvv, paymentMethod);
    }
}