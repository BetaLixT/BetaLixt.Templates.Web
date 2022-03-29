using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BetaLixT.Templates.Web.Standard.Data.Contexts;
using BetaLixT.Templates.Web.Standard.Data.Repositories;
using BetaLixT.Templates.Web.Standard.Data.Initializers;
using BetaLixT.Templates.Web.Standard.Utility.Startup;

namespace BetaLixT.Templates.Web.Standard.Data
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            // TODO Configure Correct database
            // - Entity framework database context registrations
            // -- In Memory (Only for testing purposes)
            services.AddDbContextFactory<DatabaseContext>(options => options.UseInMemoryDatabase("SampleDatabase"));
            // -- MSSQL
            /*
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration["DBConnectionString"]));
            */

            // - Registering services
            services.AddTransient<TodoRepository>();

            // - Register initializers
            services.AddSingleton<IInitializer, Seeder>();
            return services;
        }
    }
}