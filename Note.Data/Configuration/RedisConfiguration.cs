using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Note.Data.Configuration
{
    public static class RedisConfiguration
    {
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            services.AddScoped<IDatabase>(cfg =>
            {
                ConnectionMultiplexer m = ConnectionMultiplexer.Connect("redis-14095.c323.us-east-1-2.ec2.redns.redis-cloud.com:14095,password=nzXnvMTwp9O3HQGpqjpkKF6vrvYNDoIO,username=default");
                return m.GetDatabase();
            });

            return services;
        }
    }
}
