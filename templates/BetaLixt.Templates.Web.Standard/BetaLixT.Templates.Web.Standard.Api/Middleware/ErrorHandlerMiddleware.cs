using BetaLixT.Templates.Web.Standard.Utility;
using BetaLixT.Templates.Web.Standard.Utility.Exceptions;
using BetaLixT.Templates.Web.Standard.Api.Models.Responses;
using Newtonsoft.Json;

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
					case TransactionUnauthorizedException e:
						this._logger.LogWarning(e, "Unauthorized Transaction");
						ctx.Response.StatusCode = 401;
						messageBody = JsonConvert.SerializeObject(
								new FailedResponseContent{
									StatusMessage = ResponseContentStatusMessages.Unauthorized,
									ErrorMessage = e.Message,
									ErrorDetails = e.ErrorDetails,
									ErrorCode = e.ErrorCode,
									TransactionId = ctx.TraceIdentifier,
								});
						break;
					case EntityMissingException e:
						this._logger.LogWarning(e, "Entity Missing");
						ctx.Response.StatusCode = 404;
						messageBody = JsonConvert.SerializeObject(
								new FailedResponseContent{
									StatusMessage = ResponseContentStatusMessages.ResourceNotFound,
									ErrorMessage = e.Message,
									ErrorDetails = e.ErrorDetails,
									ErrorCode = e.ErrorCode,
									TransactionId = ctx.TraceIdentifier,
								});
						break;
					case TransactionCheckFailedException e:
						this._logger.LogWarning(e, "Checks Failed");
						ctx.Response.StatusCode = 400;
						messageBody = JsonConvert.SerializeObject(
								new FailedResponseContent{
									StatusMessage = ResponseContentStatusMessages.BadRequest,
									ErrorMessage = e.Message,
									ErrorDetails = e.ErrorDetails,
									ErrorCode = e.ErrorCode,
									TransactionId = ctx.TraceIdentifier,
								});
						break;
					case TransactionForbiddenException e:
						this._logger.LogWarning(e, "Transaction Failed");
						ctx.Response.StatusCode = 403;
						messageBody = JsonConvert.SerializeObject(
								new FailedResponseContent{
									StatusMessage = ResponseContentStatusMessages.Forbidden,
									ErrorMessage = e.Message,
									ErrorDetails = e.ErrorDetails,
									ErrorCode = e.ErrorCode,
									TransactionId = ctx.TraceIdentifier,
								});
						break;
					default:
						// TODO User defined exception handlers
						this._logger.LogError(ex, "Unexpected Exception Encountered");
						ctx.Response.StatusCode = 403;
						messageBody = JsonConvert.SerializeObject(
								new FailedResponseContent{
									StatusMessage = ResponseContentStatusMessages.Exception,
									ErrorCode = (int)ErrorCodes.UnexpectedError,
									TransactionId = ctx.TraceIdentifier,
								});
						break;
				}

				await ctx.Response.WriteAsync(messageBody);
			}
		}	
    }
}
