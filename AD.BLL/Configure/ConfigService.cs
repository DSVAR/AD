using System.Net.Security;
using AD.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AD.BLL.Configure
{
    public static class ConfigService
    {
        public static IServiceCollection InitService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                    b => b.MigrationsAssembly("AD.Data")
                );
            });
            return services;
        }
    }
}