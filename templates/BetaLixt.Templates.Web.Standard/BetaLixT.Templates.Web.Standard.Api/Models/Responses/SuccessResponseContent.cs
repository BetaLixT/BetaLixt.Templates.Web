using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;
using Newtonsoft.Json;

namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class SuccessResponseContent
    {
        public SuccessResponseContent(ITransferObject data)
        {
            this.resultData = data; 
        }

        public SuccessResponseContent(List<ITransferObject> data)
        {
            this.resultDataList = data;
        }

        private ITransferObject? resultData { get; set; }
        private List<ITransferObject>? resultDataList { get; set; }


        public async Task<JsonTextWriter> ToJsonAsync(JsonTextWriter writer)
        {
            await writer.WriteStartObjectAsync();
            await writer.WritePropertyNameAsync("StatusMessage");
            await writer.WriteValueAsync(ResponseContentStatusMessages.Success);

            if (this.resultData != null)
            {
                await writer.WritePropertyNameAsync("ResultData");
                writer = await this.resultData.ToJsonAsync(writer);
            }
            else if (this.resultDataList != null)
            {
                await writer.WritePropertyNameAsync("ResultData");
                await writer.WriteStartArrayAsync();
                foreach(var data in this.resultDataList)
                {
                    writer = await data.ToJsonAsync(writer); 
                } 
                await writer.WriteEndArrayAsync();
            }

            await writer.WriteEndObjectAsync();

            return writer;
        }
    }
}
