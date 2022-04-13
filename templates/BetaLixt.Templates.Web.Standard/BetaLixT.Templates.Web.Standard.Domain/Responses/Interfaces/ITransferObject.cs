using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces
{
    public interface ITransferObject
    {
        Task<JsonTextWriter> ToJsonAsync(JsonTextWriter writer);
        
        static IDictionary<string, OpenApiSchema> GetOpenApiProperties()
        {
            throw new NotImplementedException();
        }
    }
}
