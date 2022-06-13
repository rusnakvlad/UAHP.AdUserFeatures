using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IMongoContext, MongoContext>();
        return services;
    }
}
