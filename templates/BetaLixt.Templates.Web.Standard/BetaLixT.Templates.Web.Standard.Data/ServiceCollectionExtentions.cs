using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BetaLixT.Templates.Web.Standard.Data.Contexts;
using BetaLixT.Templates.Web.Standard.Data.Repositories;

namespace BetaLixT.Templates.Web.Standard.Data
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            // TODO Configure Correct database
            // - Entity framework database context registrations
            // -- In Memory (Only for testing purposes)
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("SampleDatabase"));
            // -- MSSQL
            /*
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration["DBConnectionString"]));
            */

            // - Registering services
            services.AddTransient<TodoRepository>();
            return services;
        }
    }
}