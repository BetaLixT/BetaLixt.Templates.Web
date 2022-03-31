using BetaLixT.Templates.Web.Standard.Api.Filters.Options;

namespace BetaLixT.Templates.Web.Standard.Api
{
    public static class ServiceCollectionExtentions
    {
       public static IServiceCollection RegisterApiServices(
           this IServiceCollection services,
           IConfiguration configuration
           )
       {
           services.AddDistributedMemoryCache();
           services.Configure<ResponseCacheFilterOptions>(
               configuration.GetSection(ResponseCacheFilterOptions.OptionsKey));
           return services;
       }
    }
}