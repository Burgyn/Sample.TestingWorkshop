using Kros.Extensions;

namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Working hour.
/// </summary>
public readonly record struct WorkingHour: ITimeSlot
{
    public WorkingHour(DayOfWeek dayOfWeek, TimeSpan @from, TimeSpan to)
    {
        Validate(@from, to);
        DayOfWeek = dayOfWeek;
        From = @from;
        To = to;
    }

    /// <summary>
    /// Gets or sets the day of week.
    /// </summary>
    public DayOfWeek DayOfWeek { get; }

    /// <summary>
    /// Gets or sets from.
    /// </summary>
    public TimeSpan From { get; }

    /// <summary>
    /// Gets or sets to.
    /// </summary>
    public TimeSpan To { get; }

    /// <summary>
    /// Gets the open time.
    /// </summary>
    public TimeSpan OpenTime => To - From;

    /// <summary>
    /// Is open?
    /// </summary>
    /// <param name="timeSlot">Time slot.</param>
    public bool IsOpen(TimeSlot timeSlot)
        => From <= timeSlot.From && To >= timeSlot.To;

    private static void Validate(TimeSpan from, TimeSpan to)
    {
        if (from >= to)
        {
            throw new BusinessRuleValidationException(
                "From: '{0}' be greater than To: '{1}'.".Format(from, to));
        }
    }

    /// <summary>
    /// ToString.
    /// </summary>
    public override string ToString() => $"{DayOfWeek} ({From:c} - {To:c})";
}