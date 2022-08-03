using Kros.Extensions;

namespace Reservation.Domains.Validators;

/// <summary>
/// A validator that verifies if the reservation is for the future.
/// </summary>
/// <seealso cref="IReservationValidator" />
public class FutureReservationValidator : IReservationValidator
{
    /// <summary>
    /// Validates the reservation.
    /// </summary>
    /// <param name="reservation">The reservation.</param>
    /// <exception cref="BusinessRuleValidationException">Occured when trying create reservation to the past.</exception>
    public Task ValidateAsync(Reservation reservation)
    {
        var now = reservation.Company!.Now;

        if (reservation.DateTime < now)
        {
            throw new BusinessRuleValidationException(
                "It is not possible to make a reservation to the past. Current: {0}".Format(now));
        }

        return Task.CompletedTask;
    }
}