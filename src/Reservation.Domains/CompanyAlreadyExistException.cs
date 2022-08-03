namespace Reservation.Domains;

/// <summary>
/// Occured when trying register company with existing web name.
/// </summary>
/// <seealso cref="System.Exception" />
public class CompanyAlreadyExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyAlreadyExistException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public CompanyAlreadyExistException(string message) : base(message)
    {
    }
}