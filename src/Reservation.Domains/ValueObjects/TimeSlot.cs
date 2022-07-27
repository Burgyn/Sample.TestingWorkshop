namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Time slot.
/// </summary>
public struct TimeSlot : ITimeSlot
{
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public TimeSlot(TimeSpan from, TimeSpan to)
    {
        From = from;
        To = to;
    }

    /// <summary>
    /// From.
    /// </summary>
    public TimeSpan From { get; set; }

    /// <summary>
    /// To.
    /// </summary>
    public TimeSpan To { get; set; }

    /// <summary>
    /// Create.
    /// </summary>
    public static TimeSlot Create(int fromHours, int fromMinutes, int toHours, int toMinutes) =>
        new(new TimeSpan(fromHours, fromMinutes, 0), new TimeSpan(toHours, toMinutes, 0));

    /// <summary>
    /// ToString.
    /// </summary>
    public override string ToString() => $"({From:c} - {To:c})";
}