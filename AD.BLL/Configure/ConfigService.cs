using System.Net.Security;
using AD.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AD.BLL.Interfaces;
using AD.BLL.Services;
using AD.Data.Interfaces;
using AD.Data.Repositories;
using AD.Data.Models;

namespace AD.BLL.Configure
{
    public static class ConfigService
    {
        public static IServiceCollection InitService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAutoMapper(typeof(ConfigureMapping));
            services.AddTransient<ConfigureMapping>();
          //  services.AddSingleton(typeof(IUserService), typeof(UserService));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWorkRepo>();
          //  services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWorkRepo));
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Local"),
                    b => b.MigrationsAssembly("AD.Data")
                );
            });


            return services;
        }
    }
}