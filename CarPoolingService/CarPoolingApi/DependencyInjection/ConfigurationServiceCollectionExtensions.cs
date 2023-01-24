using CarPoolingInfrastructure;
using Lib.Mongo;

namespace CarPoolingApi.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddMongoContext<CarPoolingDbContext>(config.GetConnectionString("CarPoolingDb"));

            services.AddHealthChecks();

            return services;
        }
    }
}
