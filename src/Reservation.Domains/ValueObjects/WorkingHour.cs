using Kros.Extensions;

namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Working hour.
/// </summary>
public class WorkingHour: ITimeSlot
{
    private TimeSpan? _from;
    private TimeSpan? _to;

    /// <summary>
    /// Gets or sets the day of week.
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>
    /// Gets or sets from.
    /// </summary>
    public TimeSpan From
    {
        get => _from.Value;
        set
        {
            if (_to != null)
            {
                Validate(value, To);
            }
            _from = value;
        }
    }

    /// <summary>
    /// Gets or sets to.
    /// </summary>
    public TimeSpan To
    {
        get => _to.Value;
        set
        {
            if (_from != null)
            {
                Validate(From, value);
            }
            _to = value;
        }
    }

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

    private void Validate(TimeSpan from, TimeSpan to)
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