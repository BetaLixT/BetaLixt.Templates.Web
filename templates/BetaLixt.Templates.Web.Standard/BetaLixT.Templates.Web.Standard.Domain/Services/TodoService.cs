using BetaLixT.Templates.Web.Standard.Data.Repositories;
using BetaLixT.Templates.Web.Standard.Data.Entities;
using BetaLixT.Templates.Web.Standard.Domain.Responses;
using System.Linq;
namespace BetaLixT.Templates.Web.Standard.Domain.Services
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepo;

        public TodoService(TodoRepository todoRepo)
        {
            this._todoRepo = todoRepo;
        }

        public IServiceResponse<Todo> ListTodo()
        {
            return new EnumerAsyncResponse<Todo>(this._todoRepo.GetListIAsyncEnumerable());
        } 
    }
}