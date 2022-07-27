namespace Reservation.Domains.ValueObjects;

/// <summary>
/// Date range.
/// </summary>
public readonly record struct DateRange
{
    /// <summary>
    /// Date range.
    /// </summary>
    /// <param name="from">From date.</param>
    /// <param name="to">To date.</param>
    public DateRange(DateOnly @from, DateOnly? to)
    {
        From = @from;
        To = to;
        Validate();
    }

    /// <summary>
    /// Date range.
    /// </summary>
    /// <param name="from">From date.</param>
    /// <param name="to">To date.</param>
    private DateRange(DateTime @from, DateTime? to)
        : this(DateOnly.FromDateTime(from), to is null ? null : DateOnly.FromDateTime(to.Value))
    {
    }

    /// <summary>
    /// Create from date without to.
    /// </summary>
    /// <param name="from">From.</param>
    public static DateRange Create(DateTime from)
        => new(from, null);

    /// <summary>
    /// Create from date without to.
    /// </summary>
    /// <param name="from">From.</param>
    public static DateRange Create(DateOnly from)
        => new(from, null);

    /// <summary>
    /// Create from date without to.
    /// </summary>
    /// <param name="year">Year.</param>
    /// <param name="month">Month.</param>
    /// <param name="day">Day.</param>
    public static DateRange Create(int year, int month, int day)
        => new(new DateOnly(year, month, day), null);

    /// <summary>
    /// Create date range.
    /// </summary>
    public static DateRange Create(int fromYear, int fromMonth, int fromDay, int toYear, int toMonth, int toDay)
        => new(new DateOnly(fromYear, fromMonth, fromDay), new DateOnly(toYear, toMonth, toDay));

    /// <summary>
    /// Create.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static DateRange Create(DateOnly from, DateOnly? to)
        => new(from, to);

    /// <summary>
    /// Create.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static DateRange Create(DateTime from, DateTime? to)
        => new(from, to);

    /// <summary>
    /// Range from.
    /// </summary>
    public DateOnly From { get; }

    /// <summary>
    /// Range to.
    /// </summary>
    public DateOnly? To { get; }

    /// <summary>
    /// Adds the specified number of years to the value of this instance.
    /// </summary>
    /// <param name="years">Number of years.</param>
    public DateRange AddYears(int years)
        => new(From.AddYears(years), To?.AddYears(years));

    /// <summary>
    /// Create new range with different year.
    /// </summary>
    /// <param name="year">New year.</param>
    public DateRange WithYear(int year)
        => new(new DateOnly(year, From.Month, From.Day), To is null ? null : new DateOnly(year, To.Value.Month, To.Value.Day));

    private void Validate()
    {
        if (From > (To ?? From))
        {
            throw new BusinessRuleValidationException($"From: '{From}' be greater than To: '{To}'");
        }
    }

    /// <summary>
    /// Is this range overlap with day?
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public bool IsOverlap(DateOnly date)
        => date >= From && date <= (To ?? From);
}