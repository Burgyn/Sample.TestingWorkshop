using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

public class Service
{
    /// <summary>
    /// Id of the service.
    /// </summary>
    public ServiceId Id { get; set; } = ServiceId.New();
    
    /// <summary>
    /// Gets or sets the company.
    /// </summary>
    public Company? Company { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    public TimeSpan	Duration { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public Price? Price { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is default.
    /// </summary>
    public bool IsDefault
        => string.IsNullOrEmpty(Name) || Name.Equals("NONAME SERVICE");

    /// <summary>
    /// Creates the default.
    /// </summary>
    /// <param name="company">The company.</param>
    public static Service CreateDefault(Company? company)
        => new()
        {
            Company = company,
            Name = "NONAME SERVICE",
            Duration = company?.DefaultReservationSettings?.TimeSlot ?? TimeSpan.FromMinutes(30),
            Price = Price.Default
        };
}

[StronglyTypedId]
public partial struct ServiceId
{
}