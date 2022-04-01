using BetaLixT.Templates.Web.Standard.Data.Entities;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Models.TransferObjects
{
    public class TodoListing : ITransferObject<Todo>
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
    }
}
