namespace Reservation.Domains;

public static class DateTimeExtensions
{
    /// <summary>
    /// Get easter.
    /// </summary>
    public static DateTime GetEaster(int year)
    {
        int month = 3;

        int a = year % 19 + 1;
        int b = year / 100 + 1;
        int c = (3 * b) / 4 - 12;
        int d = (8 * b + 5) / 25 - 5;
        int e = (5 * year) / 4 - c - 10;

        int f = (11 * a + 20 + d - c) % 30;
        if (f == 24)
        {
            f++;
        }

        if ((f == 25) && (a > 11))
        {
            f++;
        }

        int g = 44 - f;
        if (g < 21)
        {
            g += 30;
        }

        int day = (g + 7) - ((e + g) % 7);
        if (day <= 31)
        {
            return new DateTime(year, month, day);
        }

        day -= 31;
        month = 4;
        return new DateTime(year, month, day);
    }
}