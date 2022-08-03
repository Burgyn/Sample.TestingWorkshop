using Kros.Extensions;
using Reservation.Domains.Events;
using Reservation.Domains.Validators;
using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

public class Reservation : EntityBase
{
    private Service? _service;

    /// <summary>
    /// Id.
    /// </summary>
    public ReservationId Id { get; set; } = ReservationId.New();

    /// <summary>
    /// Gets or sets the comany.
    /// </summary>
    public Company? Company { get; set; }

    /// <summary>
    /// Gets or sets the date time of reservation.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Reservation duration.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets the client.
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Gets or sets the note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the cancellation token.
    /// </summary>
    public string? CancellationToken { get; set; }

    /// <summary>
    /// Gets or sets the service.
    /// </summary>
    public Service Service
    {
        get
        {
            if (_service is null || _service.Name.IsNullOrWhiteSpace())
            {
                _service = Service.CreateDefault(Company);
            }

            return _service;
        }
        set => _service = value;
    }

    /// <summary>
    /// Creates the new <see cref="Reservation"/>.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="reservation">Reservation source.</param>
    /// <param name="companyRepository">The company repository.</param>
    /// <param name="reservationValidators">The reservation validators.</param>
    /// <exception cref="BusinessRuleValidationException">Occurs when reservation is not valid.</exception>
    public static async Task<Reservation> CreateNewAsync(
        CompanyId companyId,
        Reservation reservation,
        ICompanyRepository companyRepository,
        IEnumerable<IReservationValidator> reservationValidators)
    {
        reservation.Company = companyRepository.GetById(companyId);

        await reservation.ValidateAsync(reservationValidators);

        reservation.CancellationToken = TokenGenerator.GenerateReservationCancellationToken();
        reservation.AddEvent(new ReservationCreated(reservation));

        return reservation;
    }

    /// <summary>
    /// Creates the new <see cref="Reservation"/>.
    /// </summary>
    /// <param name="source">Source reservation.</param>
    /// <param name="onlyDateChanged">Only date changed.</param>
    /// <param name="reservationRepository">Reservation repository.</param>
    /// <param name="reservationValidators">The reservation validators.</param>
    /// <exception cref="BusinessRuleValidationException">Occurs when reservation is not valid.</exception>
    /// <exception cref="NotFoundException"></exception>
    public static async Task<Reservation> ForUpdateAsync(
        Reservation source,
        bool onlyDateChanged,
        IReservationRepository reservationRepository,
        IEnumerable<IReservationValidator> reservationValidators)
    {
        var reservation = reservationRepository.GetById(source.Id);

        if (reservation is null)
        {
            throw new NotFoundException();
        }

        var oldDate = reservation.DateTime;
        reservation.DateTime = source.DateTime;

        if (!onlyDateChanged)
        {
            reservation.Client = source.Client;
            reservation.Duration = source.Duration;
            reservation.Note = source.Note;
            reservation.Service = source.Service;
        }

        await reservation.ValidateAsync(reservationValidators);

        if (oldDate != reservation.DateTime)
        {
            reservation.AddEvent(new ReservationDateChanged(reservation, oldDate));
        }

        return reservation;
    }

    /// <summary>
    /// Fors the cancel.
    /// </summary>
    /// <param name="reservationId">The reservation identifier.</param>
    /// <param name="reservationRepository">The reservation repository.</param>
    /// <exception cref="NotFoundException"></exception>
    public static Reservation ForCancel(
        ReservationId reservationId,
        IReservationRepository reservationRepository)
    {
        var reservation = reservationRepository.GetById(reservationId);

        if (reservation is null)
        {
            throw new NotFoundException();
        }

        reservation.AddEvent(new ReservationCancelled(reservation));

        return reservation;
    }

    private async Task ValidateAsync(IEnumerable<IReservationValidator> reservationValidators)
    {
        foreach (var validator in reservationValidators)
        {
            await validator.ValidateAsync(this);
        }
    }
}

[StronglyTypedId]
public partial struct ReservationId
{
}