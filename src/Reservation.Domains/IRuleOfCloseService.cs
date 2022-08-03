using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

/// <summary>
/// Interface which describe rule of close service.
/// </summary>
public interface IRulesOfCloseService
{
    /// <summary>
    /// Is closed in specific date time?
    /// </summary>
    /// <param name="date">Specific datetime.</param>
    /// <returns>Is closed?</returns>
    Task<bool> IsClosedAsync(DateOnly date);

    /// <summary>
    /// Is closed in specific date time?
    /// </summary>
    /// <param name="date">Specific datetime.</param>
    /// <param name="timeSlot">Specific time slot.</param>
    /// <returns>Is closed?</returns>
    Task<bool> IsClosedAsync(DateOnly date, ITimeSlot timeSlot);
}
