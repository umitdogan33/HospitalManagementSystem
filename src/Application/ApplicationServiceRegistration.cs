using System.Reflection;
using Application.Common.Behaviours.Authorization;
using Application.Common.Behaviours.Validation;
using Application.Common.Dependency;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceFactory = Application.Common.Dependency.ServiceFactory;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IServiceFactory, ServiceFactory>();

        return services;
    }
}