namespace Reservation.Domains.Events;

public record ReservationCancelled(Reservation Reservation): IReservationEvent;