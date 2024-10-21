using Microsoft.Extensions.DependencyInjection;
using Note.Data.Repository;
using StackExchange.Redis;

namespace Note.Data.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfigurationForData(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddRedis();
                
        }

        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            services
                .AddScoped<NoteDataContext>()
                .AddScoped<IDatabase>(cfg =>
                {
                    ConnectionMultiplexer m = ConnectionMultiplexer.Connect("redis-14095.c323.us-east-1-2.ec2.redns.redis-cloud.com:14095,password=nzXnvMTwp9O3HQGpqjpkKF6vrvYNDoIO");
                    return m.GetDatabase();
                });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IBookRepository, BookRepository>();
        }
    }
}
