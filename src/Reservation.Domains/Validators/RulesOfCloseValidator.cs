using Reservation.Domains.ValueObjects;

namespace Reservation.Domains.Validators;

/// <summary>
/// Validator, which validate if <see cref="Reservation"/> is in not in closed time.
/// </summary>
/// <seealso cref="IReservationValidator" />
public class RulesOfCloseValidator : IReservationValidator
{
    private readonly IRulesOfCloseService _rulesOfCloseService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="rulesOfCloseService">Rules of closed.</param>
    public RulesOfCloseValidator(IRulesOfCloseService rulesOfCloseService)
    {
        _rulesOfCloseService = rulesOfCloseService;
    }

    /// <summary>
    /// Validates the reservation.
    /// </summary>
    /// <param name="reservation">The reservation.</param>
    /// <exception cref="BusinessRuleValidationException">When reservation is out of opening hour.</exception>
    public async Task ValidateAsync(Reservation reservation)
    {
        var from = reservation.DateTime;
        var to = reservation.DateTime.AddTicks(reservation.Duration.Ticks - 1);

        if (await _rulesOfCloseService.IsClosedAsync(DateOnly.FromDateTime(reservation.DateTime),
                new TimeSlot(from.TimeOfDay, to.TimeOfDay)))
        {
            throw new BusinessRuleValidationException("Reservation cannot be completed because temporarily closed.");
        }
    }
}