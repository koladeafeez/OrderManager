
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Core.Services;
using OrderManager.Data;

namespace OrderManager.Core
{
    public static class DependecyRegistry
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWindowService, WindowService>();
            services.AddScoped<IElementService, ElementService>();
            services.AddScoped<IResponseFactory, ResponseFactory>();
            services.AddDataDependencies();

            return services;

        }
    }
}