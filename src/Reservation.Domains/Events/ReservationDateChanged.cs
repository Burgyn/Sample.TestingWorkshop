namespace Reservation.Domains.Events;

public record ReservationDateChanged(Reservation Reservation, DateTime OldDateTime) : IReservationEvent;