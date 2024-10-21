using Note.App.Controllers.Library;

namespace Note.App.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddServicesForApp(this IServiceCollection services)
        {
            return AddAutoMapperProfiles(services);
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(LibraryController.LibraryProfile));
        }
    }
}
