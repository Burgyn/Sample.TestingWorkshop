namespace Reservation.Domains;

/// <summary>
/// Business rule validation exception.
/// </summary>
/// <seealso cref="System.Exception" />
public class BusinessRuleValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BusinessRuleValidationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
    /// </summary>
    public BusinessRuleValidationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a <see langword="null" /> reference
    /// if no inner exception is specified.</param>
    public BusinessRuleValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}