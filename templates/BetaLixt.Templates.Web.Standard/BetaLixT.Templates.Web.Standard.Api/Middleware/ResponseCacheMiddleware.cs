using BetaLixT.Templates.Web.Standard.Api.Middleware.Options;
using BetaLixT.Templates.Web.Standard.Api.Middleware.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.IO.Pipelines;

namespace BetaLixT.Templates.Web.Standard.Api.Middleware
{
	class ResponseCacheMiddleware
	{
		private ResponseCacheFilterOptions _options;
		private IDistributedCache _cache;
		private RequestDelegate _next;

		public ResponseCacheMiddleware(
			RequestDelegate next,
			IDistributedCache cache,
			IOptions<ResponseCacheFilterOptions> options)
		{
			this._next = next;
			this._cache = cache;
			this._options = options.Value;
		}


		public async Task Invoke(HttpContext ctx)
		{
			var endpoint = ctx.Features.Get<IEndpointFeature>()?.Endpoint;
			var attribute = endpoint?.Metadata.GetMetadata<ResponseCacheAttribute>();

			if (attribute == null)
			{
				await this._next(ctx);
				return;
			}

			var key = 
				this._options.CacheKeyPrefix + attribute.CacheKey + string.Join(string.Empty, ctx.Request.RouteValues.Where(x => x.Key.Contains("Id", StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value));
			var resp = this._cache.Get(key);
			if (resp == null)
            {
				/*
				ctx.HttpContext.Response.StatusCode = */
				ctx.Response.Body = new MemoryStream();


				ctx.Response.BodyWriter = PipeWriter.Create(ctx.Response.Body);
				await this._next(ctx);
				/*var resStream = ctx.Response.Body;

				var age = (UInt16)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
				

				var data = new byte[resStream.Position + 3];
				var stream = new MemoryStream(data);
				stream.WriteByte((byte)ctx.Response.StatusCode);
				stream.Write(BitConverter.GetBytes(age), 0, 2);
				resStream.Position = 0;
				resStream.CopyTo(stream);
				stream.Close();
				this._cache.Set(key, data);*/
            }
			else
            {
				ctx.Response.StatusCode = resp[0];
				ctx.Response.Headers.Add("cache-age", BitConverter.ToUInt16(resp, 1).ToString());
				ctx.Response.Body.Write(resp, 3, resp.Length - 3);
            }
        }
    }
}
