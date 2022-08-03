namespace Reservation.Domains.Events;

public interface IReservationEvent : IEvent
{
    Reservation Reservation { get; }
}