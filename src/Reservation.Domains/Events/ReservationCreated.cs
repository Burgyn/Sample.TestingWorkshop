namespace Reservation.Domains.Events;

public record ReservationCreated(Reservation Reservation): IReservationEvent;