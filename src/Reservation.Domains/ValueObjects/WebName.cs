using System.Text.RegularExpressions;
using Kros.Extensions;

namespace Reservation.Domains.ValueObjects;

public record WebName
{
    private const int WebNameMaxLenght = 50;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebName"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public WebName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Name = Normalize(name);
            WasModified = string.Compare(name, Name, StringComparison.Ordinal) != 0;
            if (Name.Length > WebNameMaxLenght)
            {
                throw new BusinessRuleValidationException(
                    $"Max lenght of the WebName is {WebNameMaxLenght}.");
            }
        }
        else
        {
            throw new BusinessRuleValidationException("Web name cannot be null or white space.");
        }
    }

    /// <summary>
    /// Name usable in web url.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Was name modified?
    /// </summary>
    public bool WasModified { get; }

    /// <summary>
    /// Performs an implicit conversion from <see cref="string"/> to <see cref="WebName"/>.
    /// </summary>
    /// <param name="name">The name.</param>
    public static implicit operator WebName(string name) => new WebName(name);

    /// <summary>
    /// Performs an implicit conversion from <see cref="WebName"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="name">The name.</param>
    public static implicit operator string(WebName name) => name.Name;

    /// <summary>
    /// Converts to string.
    /// </summary>
    public override string ToString() => Name;

    private static string Normalize(string name)
    {
        name = name.RemoveDiacritics().ToLower();
        name = Regex.Replace(name, @"[^a-z0-9\s-]", "-");
        name = Regex.Replace(name, @"\s+", "-");
        name = Regex.Replace(name, @"\-{2,}", "-").Trim().Trim('-');

        return name;
    }
}