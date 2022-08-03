using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

public interface ICompanyRepository
{
    Company GetById(CompanyId id);
    
    Company GetByName(WebName name);
    
    Task CreateAsync(Company company);
    
    Task DeleteAsync(Company company);
}