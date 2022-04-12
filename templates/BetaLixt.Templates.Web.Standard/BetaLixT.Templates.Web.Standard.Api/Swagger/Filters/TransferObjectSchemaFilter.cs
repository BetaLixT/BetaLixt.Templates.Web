using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.Filters
{
    public class TransactionObjectResFilter: ISchemaFilter
    {
        public void Apply(
            Schema schema,
            SchemaRegistry registry,
            Type type)
        {
        }
    }
}
