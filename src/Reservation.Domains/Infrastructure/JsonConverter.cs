using System.ComponentModel;
using System.Text.Json;
using Kros.KORM.Converter;

namespace Reservation.Domains.Infrastructure;

/// <summary>
/// JSON Converter.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="IConverter" />
public class JsonConverter<TEntity> : IConverter
    where TEntity : new()
{
    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        IgnoreReadOnlyProperties = true,
        Converters = {
            new TimeZoneInfoConverter(),
            new TimeSpanConverter()
        }
    };

    /// <summary>
    /// Deserialize JSON from database to <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public object Convert(object value)
        => value is null
            ? new TEntity()
            : JsonSerializer.Deserialize<TEntity>((string)value, _serializerOptions);

    /// <summary>
    /// Serialize <typeparamref name="TEntity"/> to JSON.
    /// </summary>
    /// <param name="value">The value.</param>
    public object ConvertBack(object value)
        => value is null ? null : JsonSerializer.Serialize(value, _serializerOptions);
}