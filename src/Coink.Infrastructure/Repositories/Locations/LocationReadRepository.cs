using System.Data;
using Dapper;
using Coink.Application.Repositories.Locations;
using Coink.Domain.Entities;
using Coink.Infrastructure.ContextDb;

namespace Coink.Infrastructure.Repositories.Locations;

public class LocationReadRepository : ILocationReadRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public LocationReadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Country>> GetCountriesAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT * FROM user_registry.sp_get_countries();";

        using var connection = _connectionFactory.CreateConnection();

        var countries = await connection.QueryAsync<Country>(sql);
        return countries.ToList();
    }

    public async Task<IReadOnlyList<Department>> GetDepartmentsByCountryAsync(
        int countryId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT *
            FROM user_registry.sp_get_departments_by_country(@CountryId);";

        using var connection = _connectionFactory.CreateConnection();

        var departments = await connection.QueryAsync<Department>(sql, new
        {
            CountryId = countryId
        });

        return departments.ToList();
    }

    public async Task<IReadOnlyList<Municipality>> GetMunicipalitiesByDepartmentAsync(
        int departmentId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT *
            FROM user_registry.sp_get_municipalities_by_department(@DepartmentId);";

        using var connection = _connectionFactory.CreateConnection();

        var municipalities = await connection.QueryAsync<Municipality>(sql, new
        {
            DepartmentId = departmentId
        });

        return municipalities.ToList();
    }
}
