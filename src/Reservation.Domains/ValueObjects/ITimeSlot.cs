namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Interface which describe timeslot.
/// </summary>
public interface ITimeSlot
{
    /// <summary>
    /// From time.
    /// </summary>
    TimeSpan From { get; }

    /// <summary>
    /// To time.
    /// </summary>
    TimeSpan To { get; }
}