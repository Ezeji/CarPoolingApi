using CarPoolingCore.Services;
using CarPoolingCore.Services.Interfaces;

namespace CarPoolingApi.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IJourneyService, JourneyService>();
        }
    }
}
