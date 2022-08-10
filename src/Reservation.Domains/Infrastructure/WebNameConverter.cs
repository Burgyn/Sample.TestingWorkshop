using Kros.KORM.Converter;
using Reservation.Domains.ValueObjects;

namespace Reservation.Domains.Infrastructure;

internal class WebNameConverter: IConverter
{
    /// <summary>
    /// Converts the specified value from Db to Clr.
    /// </summary>
    /// <param name="value">The value.</param>
    public object Convert(object value) => new WebName((string)value);

    /// <summary>
    /// Converts the value from Clr to Db.
    /// </summary>
    /// <param name="value">The value.</param>
    public object ConvertBack(object value) => value.ToString() ?? string.Empty;
}