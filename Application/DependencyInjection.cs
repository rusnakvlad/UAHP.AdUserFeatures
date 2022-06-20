using Application.Advertisments;
using Application.Common.Interfaces;
using Application.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAdService, AdService>();
        services.AddGrpc();

        return services;
    }
}
