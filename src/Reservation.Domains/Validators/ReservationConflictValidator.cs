using Kros.Extensions;
using Kros.KORM;
using Kros.Utils;

namespace Reservation.Domains.Validators;

/// <summary>
/// A validator that verifies if the reservation is in conflict with another.
/// </summary>
public class ReservationConflictValidator : IReservationValidator
{
    private const string Sql = @"SELECT count(*)
  FROM [Reservations]
  WHERE
    CompanyId = @1 AND
    @2 < DATEADD(MINUTE, DATEDIFF(MINUTE, '0:00:00', Duration), DateTime) AND
    @3  > DateTime AND 
    (Id <> @id)";
    private readonly IDatabase _database;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="database">Database.</param>
    public ReservationConflictValidator(IDatabase database)
    {
        _database = Check.NotNull(database, nameof(database));
    }

    /// <inheritdoc/>
    public Task ValidateAsync(Reservation reservation)
    {
        var end = reservation.DateTime.Add(reservation.Duration);
        var count = GetCount(reservation, end);

        if (count > 0)
        {
            throw new ReservationConflictException(
                "In time slot ({0} - {1}) already exist reservation for the '{2}' company."
                    .Format(reservation.DateTime, end, reservation.Company!.WebName));
        }

        return Task.CompletedTask;
    }

    private int? GetCount(Reservation reservation, DateTime end) =>
        _database.ExecuteScalar<int>(
            Sql,
            reservation.Company!.Id,
            reservation.DateTime,
            end,
            reservation.Id);
}