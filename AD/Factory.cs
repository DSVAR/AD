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
            optionBuilder.UseNpgsql(config.GetConnectionString("Postgres")
                ,b => b.MigrationsAssembly("AD.Data"));
            return new ApplicationContext(optionBuilder.Options);
        }
    }
}