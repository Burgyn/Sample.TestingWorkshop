namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Reservation settings.
/// </summary>
public class ReservationSettings
{
    private TimeZoneInfo? _timeZone;

    /// <summary>
    /// Gets or sets the working hours.
    /// </summary>
    public WorkingHours WorkingHours { get; set; }

    /// <summary>
    /// Default working time slot.
    /// </summary>
    public TimeSpan TimeSlot { get; set; }

    /// <summary>
    /// Gets or sets the minimal reservation steps.
    /// </summary>
    public TimeSpan MinimalReservationSteps { get; set; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    public TimeZoneInfo TimeZone
    {
        get => _timeZone ??= TimeZoneInfo.FindSystemTimeZoneById(TimeZoneHelper.DefaultTimeZone);
        set => _timeZone = value;
    }
}