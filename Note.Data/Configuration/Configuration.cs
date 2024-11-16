using Microsoft.Extensions.DependencyInjection;
using Note.Data.RedisLibrary;
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
                    ConnectionMultiplexer m = ConnectionMultiplexer.Connect("");
                    return m.GetDatabase();
                })
                .AddScoped<IRedisKeyProvider, RedisKeyProvider>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<ICharacterRepostiory, CharacterRepository>()
                .AddScoped<INoteRepository, NoteRepository>();
        }
    }
}
