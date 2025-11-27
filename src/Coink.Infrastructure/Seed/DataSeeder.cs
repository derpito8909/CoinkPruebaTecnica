using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Extensions.Logging;
using Coink.Infrastructure.ContextDb;

namespace Coink.Infrastructure.Seed;

public class DataSeeder : IDataSeeder
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(IDbConnectionFactory connectionFactory, ILogger<DataSeeder> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting database seed...");

        using var connection = _connectionFactory.CreateConnection();

        if (connection is DbConnection dbConnection)
        {
            await dbConnection.OpenAsync(cancellationToken);
        }
        else
        {
            connection.Open();
        }

        using var transaction = connection.BeginTransaction();

        try
        {
            var colombiaId = await EnsureCountryAsync(connection, transaction, "Colombia", "CO");
            var mexicoId = await EnsureCountryAsync(connection, transaction, "México", "MX");

            var antioquiaId = await EnsureDepartmentAsync(connection, transaction, "Antioquia", colombiaId);
            var cundinamarcaId = await EnsureDepartmentAsync(connection, transaction, "Cundinamarca", colombiaId);

            var cdmxId = await EnsureDepartmentAsync(connection, transaction, "Ciudad de México", mexicoId);
            var jaliscoId = await EnsureDepartmentAsync(connection, transaction, "Jalisco", mexicoId);

            await EnsureMunicipalityAsync(connection, transaction, "Medellín", antioquiaId);
            await EnsureMunicipalityAsync(connection, transaction, "Bello", antioquiaId);
            await EnsureMunicipalityAsync(connection, transaction, "Bogotá", cundinamarcaId);

            await EnsureMunicipalityAsync(connection, transaction, "Guadalajara", jaliscoId);
            await EnsureMunicipalityAsync(connection, transaction, "Zapopan", jaliscoId);
            await EnsureMunicipalityAsync(connection, transaction, "Coyoacán", cdmxId);

            transaction.Commit();
            _logger.LogInformation("Database seed completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during database seed. Rolling back transaction.");
            transaction.Rollback();
            throw;
        }
    }

    private async Task<int> EnsureCountryAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        string name,
        string isoCode)
    {
        const string selectSql = @"
            SELECT id
            FROM coink_app.country
            WHERE iso_code = @IsoCode;";

        var existingId = await connection.ExecuteScalarAsync<int?>(
            selectSql,
            new { IsoCode = isoCode },
            transaction);

        if (existingId.HasValue)
            return existingId.Value;

        const string insertSql = @"
            INSERT INTO coink_app.country (name, iso_code)
            VALUES (@Name, @IsoCode)
            RETURNING id;";

        var newId = await connection.ExecuteScalarAsync<int>(
            insertSql,
            new { Name = name, IsoCode = isoCode },
            transaction);

        _logger.LogInformation("Inserted Country {Name} ({IsoCode}) with Id {Id}", name, isoCode, newId);

        return newId;
    }

    private async Task<int> EnsureDepartmentAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        string name,
        int countryId)
    {
        const string selectSql = @"
            SELECT id
            FROM coink_app.department
            WHERE country_id = @CountryId
              AND name = @Name;";

        var existingId = await connection.ExecuteScalarAsync<int?>(
            selectSql,
            new { CountryId = countryId, Name = name },
            transaction);

        if (existingId.HasValue)
            return existingId.Value;

        const string insertSql = @"
            INSERT INTO coink_app.department (name, country_id)
            VALUES (@Name, @CountryId)
            RETURNING id;";

        var newId = await connection.ExecuteScalarAsync<int>(
            insertSql,
            new { Name = name, CountryId = countryId },
            transaction);

        _logger.LogInformation("Inserted Department {Name} for CountryId {CountryId} with Id {Id}", name, countryId, newId);

        return newId;
    }

    private async Task<int> EnsureMunicipalityAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        string name,
        int departmentId)
    {
        const string selectSql = @"
            SELECT id
            FROM coink_app.municipality
            WHERE department_id = @DepartmentId
              AND name = @Name;";

        var existingId = await connection.ExecuteScalarAsync<int?>(
            selectSql,
            new { DepartmentId = departmentId, Name = name },
            transaction);

        if (existingId.HasValue)
            return existingId.Value;

        const string insertSql = @"
            INSERT INTO coink_app.municipality (name, department_id)
            VALUES (@Name, @DepartmentId)
            RETURNING id;";

        var newId = await connection.ExecuteScalarAsync<int>(
            insertSql,
            new { Name = name, DepartmentId = departmentId },
            transaction);

        _logger.LogInformation("Inserted Municipality {Name} for DepartmentId {DepartmentId} with Id {Id}", name, departmentId, newId);

        return newId;
    }
}
