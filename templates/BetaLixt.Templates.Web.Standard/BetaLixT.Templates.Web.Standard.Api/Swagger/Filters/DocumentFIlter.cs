using BetaLixT.Templates.Web.Standard.Api.Swagger.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Linq;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.OperationFilters
{
    public class DocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tos = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace != null && x.Namespace.Contains("TransferObjects"))
                .Where(x => x.GetInterface(nameof(ITransferObject)) != null)
                .ToList();
            
            /* context.SchemaRepository.AddDefinition() */
        }
    }
}
