using BetaLixT.Templates.Web.Standard.Data.Entities;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
namespace BetaLixT.Templates.Web.Standard.Api.Models.TransferObjects
{
    public class TodoListing : ITransferObject
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public bool IsDone { get; set; }

        public static TodoListing Map(Todo todo)
        {
            return new TodoListing
            {
                Id = todo.Id,
                Title = todo.Title,
                IsDone = todo.IsDone
            };
        }

        public async Task<JsonTextWriter> ToJsonAsync(JsonTextWriter writer)
        {
            await writer.WriteStartObjectAsync();
            await writer.WritePropertyNameAsync("id");
            await writer.WriteValueAsync(this.Id);

            await writer.WritePropertyNameAsync("title");
            await writer.WriteValueAsync(this.Title);

            await writer.WritePropertyNameAsync("isDone");
            await writer.WriteValueAsync(this.IsDone);
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
