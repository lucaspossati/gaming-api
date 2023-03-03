
using api.Profiles;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace api.Configuration
{
    public static class DbConfig
    {
        public static void ConfigDb(this IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
            });

            services.AddScoped<DataContext, DataContext>();
        }
    }
}
