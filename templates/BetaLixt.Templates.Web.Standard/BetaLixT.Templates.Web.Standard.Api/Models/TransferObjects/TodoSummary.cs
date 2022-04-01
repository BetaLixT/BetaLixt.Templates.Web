using BetaLixT.Templates.Web.Standard.Data.Entities;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Api.Models.TransferObjects
{
    public class TodoSummary : ITransferObject<Todo>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }

        public static TodoSummary Map(Todo todo)
        {
            return new TodoSummary
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsDone = todo.IsDone
            };
        }
    }
}
