namespace Reservation.Domains.Validators;

/// <summary>
/// Validator which validate <see cref="Client"/> of <see cref="Reservation"/>.
/// </summary>
/// <seealso cref="IReservationValidator" />
public class ReservationClientValidator : IReservationValidator
{
    /// <summary>
    /// Validates the reservation.
    /// </summary>
    /// <param name="reservation">The reservation.</param>
    /// <exception cref="BusinessRuleValidationException">When <see cref="Client"/> is not valid.</exception>
    public Task ValidateAsync(Reservation reservation)
    {
        if (reservation.Client is null)
        {
            throw new BusinessRuleValidationException("Client of reservation must be set");
        }
        if (reservation.Client.Email is null)
        {
            throw new BusinessRuleValidationException("Client's email must be set.");
        }

        return Task.CompletedTask;
    }
}