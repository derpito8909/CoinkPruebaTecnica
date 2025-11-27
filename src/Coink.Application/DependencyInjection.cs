using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Coink.Application.Common.Mappings;
using Coink.Application.Common.Behaviors;

namespace Coink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);

        // AutoMapper: registra todos los profiles de este ensamblado
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // FluentValidation: registra todos los validators de este ensamblado
        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
