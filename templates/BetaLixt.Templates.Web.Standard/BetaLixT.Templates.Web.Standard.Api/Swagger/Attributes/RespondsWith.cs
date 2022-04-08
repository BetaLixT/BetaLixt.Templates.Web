using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.Attributes
{
    public class RespondsWith: Attribute
    {
        public int StatusCode { get; }
        public ITransferObject Response { get; }
        RespondsWith(int statusCode, ITransferObject response)
        {
            this.StatusCode = statusCode;
            this.Response = response;
        }
    }
}
