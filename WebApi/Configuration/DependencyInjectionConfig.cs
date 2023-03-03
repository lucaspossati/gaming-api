using api.Domain.Services;
using api.Domain.Services.Interfaces;
using Data.Repository;
using Data.Repository.Interface;

namespace api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}
