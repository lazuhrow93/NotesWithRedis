using Microsoft.Extensions.DependencyInjection;

namespace Note.Domain.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfigurationForDomain(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddEntitySync()
                .AddAutoMappers();
        }

        private static IServiceCollection AddEntitySync(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IEntitySync, EntitySync>();
        }

        private static IServiceCollection AddAutoMappers(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddAutoMapper(typeof(EntitySyncProfile));
        }
    }
}
