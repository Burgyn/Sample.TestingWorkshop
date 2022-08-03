using System.Collections.ObjectModel;

namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Class which hold working hours for whole week.
/// </summary>
public class WorkingHours : Collection<WorkingHour>
{
    /// <inheritdoc/>
    protected override void InsertItem(int index, WorkingHour item)
    {
        if (Items.Any(w => w.DayOfWeek == item.DayOfWeek && w.IsOverlap(item)))
        {
            throw new BusinessRuleValidationException("Working hours in day cannot overlap.");
        }
        base.InsertItem(index, item);
    }

    /// <summary>
    /// Add working block.
    /// </summary>
    /// <param name="dayOfWeek">Day of week.</param>
    /// <param name="from">Open from.</param>
    /// <param name="to">Open to.</param>
    public WorkingHours Add(DayOfWeek dayOfWeek, string from, string to)
    {
        Add(new WorkingHour(dayOfWeek, TimeSpan.Parse(from), TimeSpan.Parse(to)));

        return this;
    }

    /// <summary>
    /// Check if in dateTime is open for specific duration.
    /// </summary>
    /// <param name="dateTime">Date time.</param>
    /// <param name="duration">Reservation duration.</param>
    /// <return>True if in specific datetime is open; otherwise false.</return>
    public bool IsOpen(DateTime dateTime, TimeSpan duration)
    {
        var timeSlot = new TimeSlot(dateTime.TimeOfDay, dateTime.TimeOfDay.Add(duration));

        return Items.Any(w => w.DayOfWeek == dateTime.DayOfWeek && w.IsOpen(timeSlot));
    }

    /// <summary>
    /// Determines whether [is working day] [the specified date time].
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns>
    ///   <c>true</c> if [is working day] [the specified date time]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsWorkingDay(DateTime dateTime)
        => Items.Any(w => w.DayOfWeek == dateTime.DayOfWeek);

    /// <summary>
    /// Get working hours for specific day.
    /// </summary>
    /// <param name="date">Specific day.</param>
    public IEnumerable<WorkingHour> ForDay(DateTime date)
        => Items.Where(w => w.DayOfWeek == date.DayOfWeek);
}