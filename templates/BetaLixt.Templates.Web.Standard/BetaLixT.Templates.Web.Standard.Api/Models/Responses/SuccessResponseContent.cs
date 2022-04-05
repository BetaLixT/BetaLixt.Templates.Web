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

        public SuccessResponseContent(IEnumerable<ITransferObject> data, int totalCount)
        {
            this.resultDataList = data;
            this._totalListCount = totalCount;
        }

        private ITransferObject? resultData;
        private IEnumerable<ITransferObject>? resultDataList;
        private readonly int? _totalListCount;


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
                await writer.WritePropertyNameAsync("TotalCount");
                await writer.WriteValueAsync(this._totalListCount);
            }

            await writer.WriteEndObjectAsync();

            return writer;
        }
    }
}
