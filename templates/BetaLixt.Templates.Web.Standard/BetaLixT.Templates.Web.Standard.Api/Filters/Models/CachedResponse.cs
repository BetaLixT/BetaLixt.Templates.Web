namespace BetaLixT.Templates.Web.Standard.Api.Filters.Models
{
    public class CachedResponse
    {
        public int StatusCode { get; set; }
        public string Data { get; set; } = "";
        public long LastFetched { get; set; }
    }
}
