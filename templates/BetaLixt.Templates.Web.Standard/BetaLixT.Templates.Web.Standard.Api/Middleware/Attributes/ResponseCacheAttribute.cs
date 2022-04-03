namespace BetaLixT.Templates.Web.Standard.Api.Middleware.Attributes
{
    public class ResponseCacheAttribute : Attribute
    {
        public string CacheKey { get; set; } = "tm";
        public int ExpirySeconds { get; set; }

        public ResponseCacheAttribute(string cacheKey, int expirySeconds)
        {
            this.CacheKey = cacheKey;
            this.ExpirySeconds = expirySeconds;
        }
    }
}
