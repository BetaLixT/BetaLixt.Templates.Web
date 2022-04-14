using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.Filters
{
    public class TransferObjectResFilter: ISchemaFilter
    {
        public void Apply(
            OpenApiSchema schema,
            SchemaFilterContext ctx)
        {
            
            if (ctx.Type.GetInterface(nameof(ITransferObject)) != null)
            {
                var props = (IDictionary<string, OpenApiSchema>)(ctx.Type.GetMethod(nameof(ITransferObject.GetOpenApiProperties))!
                    .Invoke(null, null)!);
                schema.Properties = props;
            }
        }
    }
}
