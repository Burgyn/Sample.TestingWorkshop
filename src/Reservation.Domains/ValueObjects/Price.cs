namespace Reservation.Domains.ValueObjects;

public record Price(string Currency, decimal Amount)
{
    public static readonly Price Default = new("Eur", 0);
    
    public static Price operator +(Price a, Price b)
    {
        ValidateCurrency(a, b);
        return a with { Amount = a.Amount + b.Amount };
    }
    
    public static Price operator -(Price a, Price b)
    {
        ValidateCurrency(a, b);
        return a with { Amount = a.Amount - b.Amount };
    }
    
    public static Price operator *(Price a, Price b)
    {
        ValidateCurrency(a, b);
        return a with { Amount = a.Amount * b.Amount };
    }
    
    public static Price operator /(Price a, Price b)
    {
        ValidateCurrency(a, b);
        if (b.Amount == 0)
        {
            throw new DivideByZeroException();
        }
        return a with { Amount = a.Amount / b.Amount };
    }

    private static void ValidateCurrency(Price a, Price b)
    {
        if (a.Currency != b.Currency)
        {
            throw new InvalidOperationException("Currency mismatch.");
        }
    }
}