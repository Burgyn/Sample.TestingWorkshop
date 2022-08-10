using Reservation.Domains;
using Reservation.Domains.ValueObjects;

public class Checker : ICompanyWebNameChecker
{
    public bool ExistWebName(WebName? webName)
    {
        return false;
    }

    public bool ExistWebName(WebName? webName, long companyId)
    {
        return false;
    }
}