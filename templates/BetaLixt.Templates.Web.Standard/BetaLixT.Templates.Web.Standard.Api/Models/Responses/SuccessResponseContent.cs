using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;
using Newtonsoft.Json;

namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class SuccessResponseContent
    {
        public SuccessResponseContent(ITransferObject data)
        {
            this.ResultData = data;
        }
        public ITransferObject ResultData { get; set; }


        public JsonTextWriter ToJson(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("StatusMessage");
            writer.WriteValue(ResponseContentStatusMessages.Success);

            writer.WritePropertyName("ResultData");
            writer = ResultData.ToJson(writer);

            writer.WriteEndObject();

            return writer;
        }
    }
}
