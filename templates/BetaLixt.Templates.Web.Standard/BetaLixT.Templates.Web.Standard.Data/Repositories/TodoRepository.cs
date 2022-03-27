using BetaLixT.Templates.Web.Standard.Data.Contexts;
using BetaLixT.Templates.Web.Standard.Data.Entities;
using System.Linq;
namespace BetaLixT.Templates.Web.Standard.Data.Repositories
{
    public class TodoRepository
    {
        private readonly DatabaseContext _context;
        
        public TodoRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public IAsyncEnumerable<Todo> GetTodoQueryable()
        {
            return this._context.Todos.AsAsyncEnumerable();
        } 
    }
}