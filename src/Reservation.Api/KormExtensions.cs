using System.Net.Sockets;
using Kros.KORM.Extensions.Asp;
using Microsoft.Data.SqlClient;
using Polly;

namespace Reservation.Api;

/// <summary>
/// <see cref="KormBuilder"/> extensions.
/// </summary>
public static class KormExtensions
{
    /// <summary>
    /// Migrate database with retry policy.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static KormBuilder MigrateWithRetrying(this KormBuilder builder)
    {
        var policy = Policy
            .Handle<SqlException>()
            .OrInner<SocketException>()
            .OrInner<SqlException>()
            .WaitAndRetry(15, retryAttempt =>
            {
                Console.WriteLine($"=== Migrate retry attempt: {retryAttempt}");
                return TimeSpan.FromSeconds(2);
            }, (exception, _) =>
            {
                string innerException = exception.InnerException is null
                    ? string.Empty
                    : $" InnerException: '{exception.InnerException.Message}'";
                Console.WriteLine($"=== Migration partially failed: '{exception.Message}'.{innerException}");
            });

        policy.Execute(builder.Migrate);

        return builder;
    }
}