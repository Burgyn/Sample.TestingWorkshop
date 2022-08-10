using Kros.Utils;
using Kros.Extensions;
using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

/// <summary>
/// Company model.
/// </summary>
public class Company
{
    /// <summary>
    /// Id.
    /// </summary>
    public CompanyId Id { get; set; } = CompanyId.New();

    /// <summary>
    /// Company name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Url path.
    /// </summary>
    public WebName? WebName { get; set; }

    /// <summary>
    /// Gets or sets the default reservation settings.
    /// </summary>
    /// <value>
    /// The default reservation settings.
    /// </value>
    public ReservationSettings DefaultReservationSettings { get; set; }

    /// <summary>
    /// Create new registered company.
    /// </summary>
    /// <param name="name">Company display name.</param>
    /// <param name="webName">Company web name.</param>
    /// <param name="defaultReservationSettings">Default reservation settings.</param>
    /// <param name="webNameChecker">The web name checker.</param>
    /// <returns>New company.</returns>
    /// <exception cref="CompanyAlreadyExistException">If company with web name already exist.</exception>
    public static Company CreateNewCompany(
        string name,
        WebName webName,
        ReservationSettings defaultReservationSettings,
        ICompanyWebNameChecker webNameChecker)
    {
        var newCompany = new Company()
        {
            Name = name,
            WebName = webName,
            DefaultReservationSettings = defaultReservationSettings
        };

        newCompany.ValidateNewCompany(webNameChecker);

        return newCompany;
    }

    /// <summary>
    /// Create company for update.
    /// </summary>
    /// <param name="companyId">Company id.</param>
    /// <param name="name">Company display name.</param>
    /// <param name="webName">Company web name.</param>
    /// <param name="defaultReservationSettings">Default reservation settings.</param>
    /// <param name="webNameChecker">The web name checker.</param>
    /// <returns>New company.</returns>
    /// <exception cref="CompanyAlreadyExistException">If company with web name already exist.</exception>
    public static Company CreateCompanyForUpdate(
        long companyId,
        string name,
        WebName? webName,
        ReservationSettings defaultReservationSettings,
        ICompanyWebNameChecker webNameChecker)
    {
        var company = new Company()
        {
            Name = name,
            WebName = webName,
            DefaultReservationSettings = defaultReservationSettings
        };

        if (webNameChecker.ExistWebName(webName, companyId))
        {
            ThrowAlreadyExistException(webName);
        }

        return company;
    }

    /// <summary>
    /// Gets the current DateTime by company settings.
    /// </summary>
    internal DateTime Now
        => TimeZoneInfo.ConvertTimeFromUtc(DateTimeProvider.UtcNow, DefaultReservationSettings.TimeZone);

    private void ValidateNewCompany(ICompanyWebNameChecker webNameChecker)
    {
        if (webNameChecker.ExistWebName(WebName))
        {
            ThrowAlreadyExistException(WebName);
        }
    }

    private static void ThrowAlreadyExistException(WebName? name)
        => throw new CompanyAlreadyExistException("Company with web name '{0}' already exist.".Format(name));
}

[StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public partial struct CompanyId
{
}