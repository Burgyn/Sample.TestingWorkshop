namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Recurrence type.
/// </summary>
public enum RecurrenceType
{
    /// <summary>
    /// The daily.
    /// </summary>
    Daily,

    /// <summary>
    /// The weekly.
    /// </summary>
    Weekly,

    /// <summary>
    /// The monthly.
    /// </summary>
    Monthly,

    /// <summary>
    /// The yearly.
    /// </summary>
    Yearly,

    /// <summary>
    /// The custom.
    /// </summary>
    Custom
}