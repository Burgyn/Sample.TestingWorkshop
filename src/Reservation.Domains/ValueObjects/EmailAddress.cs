using System.Net.Mail;

namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Value object which represent email address.
/// </summary>
public record EmailAddress
{
    private readonly string _value;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="email">Email address.</param>
    /// <exception cref="BusinessRuleValidationException">When is not well formatted.</exception>
    public EmailAddress(string email)
    {
        Validate(email);

        _value = email;
    }
    
    private static void Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleValidationException("Email address cannot be null or white space.");
        }
        
        try
        {
            var _ = new MailAddress(email);
        }
        catch(FormatException e)
        {
            throw new BusinessRuleValidationException(e.Message);
        }
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="string"/> to <see cref="EmailAddress"/>.
    /// </summary>
    /// <param name="email">The email.</param>
    public static implicit operator EmailAddress(string email) => new EmailAddress(email);

    /// <summary>
    /// Performs an implicit conversion from <see cref="EmailAddress"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="email">The email.</param>
    public static implicit operator string(EmailAddress email) => email._value;

    /// <summary>
    /// Is testing address?
    /// </summary>
    public bool IsTestingAddress => _value?.EndsWith("reservation.test") == true
                                    || _value?.Equals("postmantest@reservation.online") == true;

    /// <summary>
    /// Converts to string.
    /// </summary>
    public override string ToString() => _value;
}