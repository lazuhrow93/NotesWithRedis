using Note.App.Controllers.Library;
using Note.App.Services;

namespace Note.App.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfigurationForApp(this IServiceCollection services)
        {
            return services
                .AddAutoMapperProfiles()
                .AddControlCenter();
        }

        private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(LibraryController.LibraryProfile));
        }

        private static IServiceCollection AddControlCenter(this IServiceCollection services)
        {
            return services.AddScoped<IControlCenter, ControlCenter>();
        }
    }
}
