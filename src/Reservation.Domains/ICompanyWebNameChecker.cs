using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

/// <summary>
/// Checker for existence company web name.
/// </summary>
public interface ICompanyWebNameChecker
{
    /// <summary>
    /// Check if exists company with <paramref name="webName"/>.
    /// </summary>
    /// <param name="webName">Web name of the company.</param>
    /// <returns>
    /// <see langword = "true" /> if the company with <paramref name="webName"/> exist; otherwise <see langword = "false" />
    /// </returns>
    bool ExistWebName(WebName? webName);

    /// <summary>
    /// Check if exists another company with <paramref name="webName"/>.
    /// </summary>
    /// <param name="webName">Web name of the company.</param>
    /// <param name="companyId">Current company id.</param>
    /// <returns>
    /// <see langword = "true" /> if the company with <paramref name="webName"/> exist; otherwise <see langword = "false" />
    /// </returns>
    bool ExistWebName(WebName? webName, long companyId);
}