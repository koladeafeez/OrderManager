using Microsoft.Extensions.DependencyInjection;
using OrderManager.Data.Models.Repositories;

namespace OrderManager.Data
{
    public static class DependencyRegistry
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;

        }
    }
}
