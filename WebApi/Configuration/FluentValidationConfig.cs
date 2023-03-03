using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace api.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        }
    }
}
