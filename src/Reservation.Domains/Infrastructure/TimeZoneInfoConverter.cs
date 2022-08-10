using System.Text.Json;

namespace Reservation.Domains.Infrastructure;

/// <summary>
/// JSON Converter for <see cref="TimeZoneInfo"/>.
/// </summary>
internal class TimeZoneInfoConverter : System.Text.Json.Serialization.JsonConverter<TimeZoneInfo>
{
    /// <summary>
    /// The default time zone
    /// </summary>
    public const string DefaultTimeZone = "Central Europe Standard Time";

    /// <summary>
    /// Writes the json.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The value.</param>
    /// <param name="options">The options.</param>
    public override void Write(
        Utf8JsonWriter writer,
        TimeZoneInfo value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }

    /// <summary>
    /// Reads the specified reader.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="options">The options.</param>
    public override TimeZoneInfo Read(
        ref Utf8JsonReader reader,
        Type objectType,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return TimeZoneInfo.FindSystemTimeZoneById(string.IsNullOrWhiteSpace(value) ? DefaultTimeZone : value);
    }
}