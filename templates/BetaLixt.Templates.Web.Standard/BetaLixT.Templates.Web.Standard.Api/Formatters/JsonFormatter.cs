using BetaLixT.Templates.Web.Standard.Api.Models.Responses;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace BetaLixT.Templates.Web.Standard.Api.Formatters
{
    public class JsonFormatter : BufferedMediaTypeFormatter
    {
        public JsonFormatter()
        {
            // Add the supported media type.
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var sw = new StreamWriter(writeStream))
            {
                var resp = value as SuccessResponseContent;
                JsonTextWriter writer = new JsonTextWriter(sw);
                writer = resp.ToJson(writer);
            }
        }
    }
}
