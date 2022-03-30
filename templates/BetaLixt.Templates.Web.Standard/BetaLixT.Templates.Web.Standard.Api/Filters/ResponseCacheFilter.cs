using Microsoft.AspNetCore.Mvc.Filters;

namespace BetaLixT.Templates.Web.Standard.Api.Filters
{
	class ResponseCacheFilter : Attribute, IActionFilter
	{
		public string CacheKey { get; set; }
		public int ExpiryMinutes { get; set; }

		public void OnActionExecuting(ActionExecutingContext ctx)
		{
		}

		public void OnActionExecuted(ActionExecutedContext ctx)
		{
		}
	}
}
