using Microsoft.Extensions.DependencyInjection;
using BetaLixT.Templates.Web.Standard.Domain.Services;

namespace BetaLixT.Templates.Web.Standard.Domain
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddTransient<TodoService>();
            return services;
        }
    }
}