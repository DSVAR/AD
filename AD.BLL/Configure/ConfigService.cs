using System.Net.Security;
using AD.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AD.BLL.Services;
using AD.Data.Interfaces;
using AD.Data.Repositories;
using AD.Data.Models;
using Microsoft.AspNetCore.Http;
using AD.BLL.Methods;
using AD.BLL.JsonPattern;

namespace AD.BLL.Configure
{
    public static class ConfigService
    {
        public static IServiceCollection InitService(IServiceCollection services, IConfiguration configuration)
        {
            //добавление сервисов
            services.AddIdentity<User, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAutoMapper(typeof(ConfigureMapping));
            services.AddTransient<ConfigureMapping>();
            services.AddScoped(typeof( UserService));
            services.AddHttpContextAccessor();
            services.AddTransient<IUnitOfWork, UnitOfWorkRepo>();
            services.AddTransient(typeof(UserMethods) );
            services.AddTransient<JsonHttpRespone>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                //рабочая/домашняя SQL в родительском проекте есть Factory, при изменении бд менять и там.
                options.UseSqlServer(configuration.GetConnectionString("Local"),
                    b => b.MigrationsAssembly("AD.Data")
                ); //Work


                //options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                //    b => b.MigrationsAssembly("AD.Data")
                //);//home


                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            return services;
        }
    }
}