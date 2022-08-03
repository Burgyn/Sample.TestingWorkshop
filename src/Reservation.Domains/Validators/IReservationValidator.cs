namespace Reservation.Domains.Validators;

/// <summary>
/// Business rule validator for <see cref="Reservation"/>.
/// </summary>
public interface IReservationValidator
{
    /// <summary>
    /// Validates the reservation.
    /// </summary>
    /// <param name="reservation">The reservation.</param>
    /// <exception cref="BusinessRuleValidationException">Occurs when reservation is not valid.</exception>
    Task ValidateAsync(Reservation reservation);
}
