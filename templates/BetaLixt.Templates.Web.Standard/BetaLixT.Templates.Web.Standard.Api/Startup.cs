using FluentValidation.AspNetCore;
using BetaLixT.Templates.Web.Standard.Api.Authentication;
using BetaLixT.Templates.Web.Standard.Api.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.AutoRegisterDi;

namespace BetaLixT.Templates.Web.Standard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // - This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // - Setting up authentication services
            services.RegisterJwtAuthenticationService(Configuration.GetSection("JwtConfig"), PolicyNames.AzureAuth);
            services.RegsiterApiKeyAuthenticationService(Configuration.GetSection("ApiKeyConfig"), PolicyNames.JobApiKey);

            // - Setting
            services.AddControllers();
            services.AddCors(options => options.AddDefaultPolicy(
               builder =>
               {
                   builder
                       .WithOrigins(
                          "https://betalixt.com"
                          )
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
               }));

            // - Automatic DI
            services.RegisterAssemblyPublicNonGenericClasses()
                .AsPublicImplementedInterfaces();

            // - Model validation service
            services.AddMvc().AddFluentValidation();

            // - Adding Swagger service
            services.AddSwaggerGen();
        }

        // - This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();
            
            // - Global error handling
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // - Adding Swagger to pipeline
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "api"));
        }
    }
}
