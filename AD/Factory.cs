using System.IO;
using AD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AD
{
    public class Factory
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            // получаем конфигурацию из файла appsettings.json
           
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build(); 
            
            var optionBuilder = new DbContextOptionsBuilder();

            //
            optionBuilder.UseSqlServer(config.GetConnectionString("Local")
                , b => b.MigrationsAssembly("AD.Data"));// work

            //optionBuilder.UseNpgsql(config.GetConnectionString("Postgre")
            //    , b => b.MigrationsAssembly("AD.Data")); //Home

            return new ApplicationContext(optionBuilder.Options);
        }
    }
}