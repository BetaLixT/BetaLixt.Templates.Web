namespace BetaLixT.Templates.Web.Standard.Api.Middleware.Options
{
    public class ResponseCacheFilterOptions
    {
        public const string OptionsKey = "ResponseCacheFilterOptions";
        public string CacheKeyPrefix { get; set; } = "Res";
    }
}
