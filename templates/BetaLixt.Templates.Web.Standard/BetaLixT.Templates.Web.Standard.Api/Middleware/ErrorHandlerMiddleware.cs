using BetaLixT.Templates.Web.Standard.Utility.Exceptions
using BetaLixT.Templates.Web.Standard.Api.Models.Responses

namespace BetaLixT.Templates.Web.Standard.Api.Middleware
{	
	class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ErrorHandlerMiddleware(
			RequestDelegate next,
			ILogger<ErrorHandlerMiddleware> logger)
		{
			this._next = next;
			this._logger = logger;
		}


		public async Task Invoke(HttpContext ctx)
		{
			try
			{
				await this._next(ctx);
			}
			catch(Exception ex)
			{
				ctx.Response.ContentType = "application/json";
				var messageBody = string.Empty;
				switch(ex)
				{
					case EntityUnauthorizedException e:

						break;
				}
			}
		}	
        }
    }
}
