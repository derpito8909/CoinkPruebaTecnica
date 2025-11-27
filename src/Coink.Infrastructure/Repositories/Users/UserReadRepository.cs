using System.Data;
using Dapper;
using Coink.Application.Repositories;
using Coink.Domain.Entities;
using Coink.Infrastructure.ContextDb;

namespace Coink.Infrastructure.Repositories.Users;

public class UserReadRepository : IUserReadRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserReadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT *
            FROM user_registry.sp_get_user_by_id(@UserId);";

        using var connection = _connectionFactory.CreateConnection();

        var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new
        {
            UserId = id
        });

        return user;
    }
}
