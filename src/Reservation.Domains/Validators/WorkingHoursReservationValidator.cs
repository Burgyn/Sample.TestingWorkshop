namespace Reservation.Domains.Validators;

/// <summary>
/// Validator, which validate if <see cref="Reservation"/> is in Working hour.
/// </summary>
/// <seealso cref="IReservationValidator" />
public class WorkingHoursReservationValidator : IReservationValidator
{
    /// <summary>
    /// Validates the reservation.
    /// </summary>
    /// <param name="reservation">The reservation.</param>
    /// <exception cref="BusinessRuleValidationException">When reservation is out of opening hour.</exception>
    public Task ValidateAsync(Reservation reservation)
    {
        if (!reservation.Company!.DefaultReservationSettings.WorkingHours.IsOpen(reservation.DateTime, reservation.Duration))
        {
            throw new BusinessRuleValidationException("The reservation cannot be completed because it is out of opening hours.");
        }

        return Task.CompletedTask;
    }
}