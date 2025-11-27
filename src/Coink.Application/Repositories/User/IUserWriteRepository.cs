using Coink.Domain.Entities;

namespace Coink.Application.Repositories;

public interface IUserWriteRepository
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}
