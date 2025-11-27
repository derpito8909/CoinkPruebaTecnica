using Coink.Domain.Entities;

namespace Coink.Application.Repositories;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
