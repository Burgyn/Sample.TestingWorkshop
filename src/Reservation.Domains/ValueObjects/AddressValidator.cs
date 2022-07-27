using FluentValidation;

namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Address validator.
/// </summary>
public class AddressValidator : AbstractValidator<Address>
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public AddressValidator()
    {
        RuleFor(x => x.City).MaximumLength(60);
        RuleFor(x => x.Country).MaximumLength(60);
        RuleFor(x => x.State).MaximumLength(60);
        RuleFor(x => x.Street).MaximumLength(60);
        RuleFor(x => x.PostalCode).MaximumLength(10);
    }
}