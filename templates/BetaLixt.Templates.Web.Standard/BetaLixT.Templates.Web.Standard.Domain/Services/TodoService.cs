using BetaLixT.Templates.Web.Standard.Data.Repositories;
using BetaLixT.Templates.Web.Standard.Data.Entities;
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

        public IAsyncEnumerable<Todo> ListTodo()
        {
            return this._todoRepo.GetListIAsyncEnumerable();
        } 
    }
}