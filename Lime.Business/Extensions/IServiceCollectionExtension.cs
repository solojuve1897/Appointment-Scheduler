using Lime.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Lime.Business
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeLogic, EmployeeLogic>();
            services.AddScoped<ISuggestionLogic, SuggestionLogic>();
            services.AddDataServices();
            return services;
        }
    }
}
