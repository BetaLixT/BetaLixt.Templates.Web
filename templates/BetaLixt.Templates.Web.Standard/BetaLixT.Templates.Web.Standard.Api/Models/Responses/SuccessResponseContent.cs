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


        public async Task<JsonTextWriter> ToJsonAsync(JsonTextWriter writer)
        {
            await writer.WriteStartObjectAsync();
            await writer.WritePropertyNameAsync("StatusMessage");
            await writer.WriteValueAsync(ResponseContentStatusMessages.Success);

            await writer.WritePropertyNameAsync("ResultData");
            writer = await ResultData.ToJsonAsync(writer);

            await writer.WriteEndObjectAsync();

            return writer;
        }
    }
}
