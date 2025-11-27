using Microsoft.Extensions.DependencyInjection;
using Coink.Application.Repositories.Locations;
using Coink.Application.Repositories;
using Coink.Infrastructure.ContextDb;
using Coink.Infrastructure.Repositories.Locations;
using Coink.Infrastructure.Repositories.Users;

namespace Coink.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IUserReadRepository, UserReadRepository>();

        services.AddScoped<ILocationReadRepository, LocationReadRepository>();

        return services;
    }
}
