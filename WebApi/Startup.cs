using api.Configuration;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigDb();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });

            services.AddDependencyInjectionConfiguration();
            services.AddAutoMapperConfiguration();
            services.AddFluentValidationConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
                });
            }

            app.UseCors(x => x
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true) // allow any origin 
                            .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
