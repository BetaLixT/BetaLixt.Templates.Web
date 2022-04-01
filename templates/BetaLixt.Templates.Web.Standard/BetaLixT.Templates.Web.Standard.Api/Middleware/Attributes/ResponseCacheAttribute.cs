namespace BetaLixT.Templates.Web.Standard.Api.Middleware.Attributes
{
    public class ResponseCacheAttribute : Attribute
    {
        public string CacheKey { get; set; } = "tm";
        public int ExpiryMinutes { get; set; }

        public ResponseCacheAttribute(string cacheKey, int expiryMinutes)
        {
            this.CacheKey = cacheKey;
            this.ExpiryMinutes = expiryMinutes;
        }
    }
}
