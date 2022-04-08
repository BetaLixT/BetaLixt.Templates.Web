using BetaLixT.Templates.Web.Standard.Api.Swagger.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.OperationFilters
{
    public class TransactionObjectResFilter : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context)
        {
            if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            {
                return;
            }

            // TODO Find a different way to do this instead of relying on reflections
            var respondsWiths = methodInfo?.DeclaringType?.CustomAttributes.OfType<RespondsWith>();
            if (respondsWiths != null)
            {
                foreach (var respondsWith in respondsWiths)
                {
                    operation.Responses.Add(
                        respondsWith.StatusCode.ToString(),
                        new OpenApiResponse { Description = "Unauthorized" });
                    new OpenApiResponse { Content = new Dictionary<string, OpenApiMediaType>() {
                        { "", new OpenApiMediaType{ 
                        }}
                    }};
                }

                if (!operation.Responses.Any(r => r.Key == StatusCodes.Status401Unauthorized.ToString()))
                {
                }
            }

        }
    }
}
