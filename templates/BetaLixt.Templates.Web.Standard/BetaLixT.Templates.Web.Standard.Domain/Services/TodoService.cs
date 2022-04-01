using BetaLixT.Templates.Web.Standard.Data.Repositories;
using BetaLixT.Templates.Web.Standard.Data.Entities;
using BetaLixT.Templates.Web.Standard.Domain.Responses;
using System.Linq;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Domain.Services
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepo;

        public TodoService(TodoRepository todoRepo)
        {
            this._todoRepo = todoRepo;
        }

        public IServiceListResponse<Todo> ListTodo()
        {
            return new EnumerableAsyncResponse<Todo>(
                this._todoRepo.GetListIAsyncEnumerable());
        }

        public IServiceResponse<Todo> GetTodo(Guid id)
        {
            return new EnumberableAsyncSingle<Todo>(
                this._todoRepo.GetListIAsyncEnumerable().Where(x => x.Id == id));
        }
    }
}