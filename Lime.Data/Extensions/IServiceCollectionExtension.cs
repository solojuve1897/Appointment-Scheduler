using Microsoft.Extensions.DependencyInjection;

namespace Lime.Data
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBusyTimeRepository, BusyTimeRepository>();
            services.AddSingleton<IDataContext, DataContext>();
            return services;
        }
    }
}
