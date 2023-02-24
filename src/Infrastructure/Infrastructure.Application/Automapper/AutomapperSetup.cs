using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Application.Automapper
{
    public static class AutomapperSetup
    {
        public static IServiceCollection AutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
