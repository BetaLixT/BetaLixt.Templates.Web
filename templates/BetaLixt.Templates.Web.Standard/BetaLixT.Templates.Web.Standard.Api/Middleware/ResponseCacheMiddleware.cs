using BetaLixT.Templates.Web.Standard.Api.Middleware.Options;
using BetaLixT.Templates.Web.Standard.Api.Middleware.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.IO.Pipelines;
using System.Net.Mime;
using System.Collections.Immutable;

namespace BetaLixT.Templates.Web.Standard.Api.Middleware
{
	class ResponseCacheMiddleware
	{
		private ResponseCacheFilterOptions _options;
		private IDistributedCache _cache;
		private RequestDelegate _next;

		// TODO add more content types
		private static readonly ImmutableDictionary<byte, string> AllowedContentType = new Dictionary<byte, string> {
			{0, MediaTypeNames.Application.Json},
			{1, $"{MediaTypeNames.Application.Json}; charset=utf-8"},
			{2, MediaTypeNames.Application.Xml},
			{3, MediaTypeNames.Application.Pdf}
		}.ToImmutableDictionary();

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
				var oriStream = ctx.Response.Body;
				ctx.Response.Body = new MemoryStream();
	
				await this._next(ctx);
				var contentType = AllowedContentType
					.Where(x => x.Value == ctx.Response.ContentType)
					.FirstOrDefault();
				var resStream = ctx.Response.Body;


				// - Caching response
				if (contentType.Value != null)
				{				
					var age = (UInt16)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
					
					var data = new byte[resStream.Position + 4];
					var stream = new MemoryStream(data);
					stream.WriteByte((byte)ctx.Response.StatusCode);
					stream.WriteByte(contentType.Key);
					stream.Write(BitConverter.GetBytes(age), 0, 2);
					resStream.Position = 0;
					resStream.CopyTo(stream);
					stream.Close();
					this._cache.Set(key, data);
				}

				// - Moving back to original stream	
				resStream.Position = 0;
				await resStream.CopyToAsync(oriStream);
				ctx.Response.Body = oriStream;
				resStream.Close();
            }
			else
            {
				ctx.Response.StatusCode = resp[0];
				ctx.Response.ContentType = AllowedContentType[resp[1]];
				ctx.Response.Headers.Add("cache-age", BitConverter.ToUInt16(resp, 2).ToString());
				await ctx.Response.Body.WriteAsync(resp, 4, resp.Length - 4);
            }
        }
    }
}
