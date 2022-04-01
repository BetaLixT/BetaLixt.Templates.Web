using BetaLixT.Templates.Web.Standard.Api.Filters.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace BetaLixT.Templates.Web.Standard.Api.Filters
{
	class ResponseCacheFilter : Attribute, IAsyncActionFilter
	{
		private ResponseCacheFilterOptions _options;
		private IDistributedCache _cache;
		public string CacheKey { get; set; } = "tm";
		public int ExpiryMinutes { get; set; }


        public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate nxt)
        {
			// TODO Should be a better way to do this...?
			this._cache = ctx.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
			this._options = ctx.HttpContext.RequestServices.GetRequiredService<IOptions<ResponseCacheFilterOptions>>().Value;

			var key = this._options.CacheKeyPrefix + CacheKey + string.Join(':', ctx.HttpContext.Request.RouteValues.Where(x => x.Key.Contains("Id", StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value));
			var resp = this._cache.Get(key);
			if (resp == null)
            {
				/*ctx.HttpContext.Response.Body = new MemoryStream();
				ctx.HttpContext.Response.StatusCode = */
				ctx.HttpContext.Response.Body = new MemoryStream();
				var executed = await nxt();
				var resStream = ctx.HttpContext.Response.BodyWriter.AsStream();
				/*executed.*/
				var age = (UInt16)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
				

				var data = new byte[resStream.Position + 3];
				var stream = new MemoryStream(data);
				stream.WriteByte((byte)ctx.HttpContext.Response.StatusCode);
				stream.Write(BitConverter.GetBytes(age), 0, 2);
				resStream.Position = 0;
				resStream.CopyTo(stream);
				stream.Close();

				// TODO Move the save to background task mayhaps
				this._cache.Set(key, resp);
            }
			else
            {
				ctx.HttpContext.Response.StatusCode = resp[0];
				ctx.HttpContext.Response.Headers.Add("cache-age", BitConverter.ToUInt16(resp, 1).ToString());
				ctx.HttpContext.Response.Body.Write(resp, 3, resp.Length - 3);
            }
        }
    }
}
