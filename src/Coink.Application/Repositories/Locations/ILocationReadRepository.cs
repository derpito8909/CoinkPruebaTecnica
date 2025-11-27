using Coink.Domain.Entities;

namespace Coink.Application.Repositories.Locations;

public interface ILocationReadRepository
{
    Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Department>> GetDepartmentsByCountryAsync(int countryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Municipality>> GetMunicipalitiesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
}
