using AD.BLL.Services;
using AD.Init;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost web= CreateHostBuilder(args).Build();

            using (var scoped = web.Services.CreateScope().ServiceProvider.CreateScope())
            {
                var deffInit = scoped.ServiceProvider.GetService<FirstAdd>();
                await deffInit.DeffAddUserRole();
            }

            web.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                });
    }
}
