using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

namespace Reservation.Domains;

/// <summary>
/// Token generator.
/// </summary>
public static class TokenGenerator
{
    /// <summary>
    /// Generates the reservation cancellation token.
    /// </summary>
    public static string GenerateReservationCancellationToken()
        => WebEncoders.Base64UrlEncode(GenerateRandomBytes(70));

    private static byte[] GenerateRandomBytes(int nrBytes)
        => RandomNumberGenerator.GetBytes(nrBytes);
}