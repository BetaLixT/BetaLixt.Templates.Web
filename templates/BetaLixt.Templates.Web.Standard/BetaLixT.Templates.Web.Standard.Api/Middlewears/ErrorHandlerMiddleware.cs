using BetaLixT.Templates.Web.Standard.Api.Models.ApiResponses;
using BetaLixT.Templates.Web.Standard.Functionality.Interface;
using BetaLixT.Templates.Web.Standard.Functionality.Interface.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BetaLixT.Templates.Web.Standard.Api.Setup
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                string messageBody = string.Empty;
                switch (error)
                {
                    case EntityCheckFailedException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        messageBody = JsonSerializer.Serialize(new FailedResponseContent
                        {
                            StatusMessage = ResponseContentStatusMessages.ValidationFailed,
                            ErrorCode = e.Code,
                            ErrorMessage = e.Message,
                        });
                        break;

                    case EntityMissingException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        messageBody = JsonSerializer.Serialize(new FailedResponseContent
                        {
                            StatusMessage = ResponseContentStatusMessages.ResourceNotFound,
                            ErrorCode = e.Code,
                            ErrorMessage = e.Message,
                        });
                        break;
                    case EntityForbiddenException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        messageBody = JsonSerializer.Serialize(new FailedResponseContent
                        {
                            StatusMessage = ResponseContentStatusMessages.ValidationFailed,
                            ErrorCode = e.Code,
                            ErrorMessage = e.Message,
                        });
                        break;
                    case EntityUnauthorizedException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        messageBody = JsonSerializer.Serialize(new FailedDetailedResponseContent
                        {
                            StatusMessage = ResponseContentStatusMessages.Exception,
                            ErrorCode = (int)ErrorCodes.UnhandledError,
                            ErrorMessage = ErrorCodes.UnhandledError.ToString(),
                            ErrorDetails = error.Message
                        });
                        break;
                }

                var result = messageBody;
                await response.WriteAsync(result);
            }
        }
    }
}
