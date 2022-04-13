using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class SuccessResponseContent : ITransferObject
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
            await writer.WritePropertyNameAsync("statusMessage");
            await writer.WriteValueAsync(ResponseContentStatusMessages.Success);

            if (this._resultData != null)
            {
                await writer.WritePropertyNameAsync("resultData");
                writer = await this._resultData.ToJsonAsync(writer);
            }
            else if (this._resultDataList != null)
            {
                await writer.WritePropertyNameAsync("resultData");
                await writer.WriteStartArrayAsync();
                foreach(var data in this._resultDataList)
                {
                    writer = await data.ToJsonAsync(writer);
                } 
                await writer.WriteEndArrayAsync();
                await writer.WritePropertyNameAsync("totalCount");
                await writer.WriteValueAsync(this._totalListCount);
            }

            await writer.WriteEndObjectAsync();

            return writer;
        }

        public OpenApiSchema GetOpenApiSchema()
        {
            return new OpenApiSchema {
                Properties = {}
            };
        }

        public string GetOpenApiKey()
        {
            return "TodoListing"; 
        }
    } 
}
