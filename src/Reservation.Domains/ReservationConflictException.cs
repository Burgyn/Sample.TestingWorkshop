namespace Reservation.Domains;

/// <summary>
/// Occurs when trying to make reservation when another one exist in that time slot.
/// </summary>
/// <seealso cref="System.Exception" />
public class ReservationConflictException : Exception
{
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="message">Message.</param>
    public ReservationConflictException(string message) : base(message)
    {
    }
}