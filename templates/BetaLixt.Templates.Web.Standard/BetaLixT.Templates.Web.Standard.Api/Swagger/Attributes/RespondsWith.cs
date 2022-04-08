using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.Attributes
{
    public class RespondsWith: Attribute
    {
        public int StatusCode { get; }
        RespondsWith(int statusCode)
        {
            this.StatusCode = statusCode;   
        }
    }
}
