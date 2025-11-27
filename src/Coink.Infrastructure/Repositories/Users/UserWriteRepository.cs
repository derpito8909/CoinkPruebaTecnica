using System.Data;
using Dapper;
using Coink.Application.Repositories;
using Coink.Domain.Entities;
using Coink.Infrastructure.ContextDb;

namespace Coink.Infrastructure.Repositories.Users;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserWriteRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User> CreateUserAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT user_registry.sp_create_user(
                @FullName,
                @Phone,
                @Address,
                @CountryId,
                @DepartmentId,
                @MunicipalityId
            );";

        using var connection = _connectionFactory.CreateConnection();

        var newId = await connection.ExecuteScalarAsync<int>(sql, new
        {
            FullName = user.FullName,
            Phone = user.Phone,
            Address = user.Address,
            CountryId = user.CountryId,
            DepartmentId = user.DepartmentId,
            MunicipalityId = user.MunicipalityId
        });

        user.SetId(newId);

        return user;
    }
}
