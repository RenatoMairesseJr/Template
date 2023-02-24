using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repository;
using Infrastructure.Repository.DatabaseConfig;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;
using Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Application.Identity.Services;
using Domain.Interfaces.Identity;
using Domain.DbModels.Identity;
using Infrastructure.Providers.Services;

namespace Infrastructure.Application.DependencyInjection
{
    /// <summary>
    /// ApiModules
    /// </summary>
    public static class ApiModules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection SetupIoC(this IServiceCollection services, IConfiguration configuration)
        {
            //Database
            var connectionString = configuration.GetSection("DbConnectionString");
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString.Value);
            }); ;

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IToDoListRepository, ToDoListRepository>();

            //Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<DatabaseContext>()
                    .AddDefaultTokenProviders();

            //Providers
            services.AddTransient<IAuthProvider, AuthProvider>();
            services.AddScoped<IToDoListProvider, ToDoListProvider>();
            services.AddScoped<IDataProtectionService, DataProtectionService>();

            return services;
        }
    }
}
