namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Extensions over <see cref="ITimeSlot"/>.
/// </summary>
public static class ITimeSlotExtensions
{
    /// <summary>
    /// Do the two time slots overlap?
    /// </summary>
    /// <param name="timeSlot1">First.</param>
    /// <param name="timeSlot2">Second.</param>
    /// <param name="inclusive">Use inclusive condition?</param>
    public static bool IsOverlap(this ITimeSlot timeSlot1, ITimeSlot timeSlot2, bool inclusive = true)
        =>
            inclusive
                ? timeSlot1.From <= timeSlot2.To && timeSlot1.To >= timeSlot2.From
                : timeSlot1.From < timeSlot2.To && timeSlot1.To > timeSlot2.From;
}