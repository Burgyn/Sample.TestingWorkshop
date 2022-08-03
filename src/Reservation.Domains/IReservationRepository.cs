namespace Reservation.Domains;

public interface IReservationRepository
{
    Reservation GetById(ReservationId id);

    Task CreateAsync(Reservation reservation);
    
    Task UpdateAsync(Reservation reservation);
    
    Task CancelAsync(ReservationId id, CompanyId companyId);
    
    Task CancelAsync(ReservationId id, string cancellationToken);
}