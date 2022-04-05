using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;
using Newtonsoft.Json;

namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class SuccessResponseContent
    {
        public SuccessResponseContent(ITransferObject data)
        {
            this._resultData = data; 
        }

        public SuccessResponseContent(IEnumerable<ITransferObject> data, int totalCount)
        {
            this._resultDataList = data;
            this._totalListCount = totalCount;
        }

        private readonly ITransferObject? _resultData;
        private readonly IEnumerable<ITransferObject>? _resultDataList;
        private readonly int? _totalListCount;


        public async Task<JsonTextWriter> ToJsonAsync(JsonTextWriter writer)
        {
            await writer.WriteStartObjectAsync();
            await writer.WritePropertyNameAsync("StatusMessage");
            await writer.WriteValueAsync(ResponseContentStatusMessages.Success);

            if (this._resultData != null)
            {
                await writer.WritePropertyNameAsync("ResultData");
                writer = await this._resultData.ToJsonAsync(writer);
            }
            else if (this._resultDataList != null)
            {
                await writer.WritePropertyNameAsync("ResultData");
                await writer.WriteStartArrayAsync();
                foreach(var data in this._resultDataList)
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
