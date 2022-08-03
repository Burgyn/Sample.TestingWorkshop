using MediatR;

namespace Reservation.Domains;

/// <summary>
/// Base class for domain class.
/// </summary>
public abstract class EntityBase
{
    private readonly List<IEvent> _events = new();

    /// <summary>
    /// Domain events.
    /// </summary>
    protected IEnumerable<IEvent> Events => _events;

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="event">Domain event.</param>
    internal void AddEvent(IEvent @event) => _events.Add(@event);

    /// <summary>
    /// Raise all domain events.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public async Task RaiseEvents(IMediator mediator)
    {
        foreach (var @event in _events)
        {
            await mediator.Publish(@event);
        }
    }
}