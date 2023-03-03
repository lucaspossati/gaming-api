
using api.Profiles;

namespace api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(CompanyProfile), 
                typeof(PersonProfile));
        }
    }
}
