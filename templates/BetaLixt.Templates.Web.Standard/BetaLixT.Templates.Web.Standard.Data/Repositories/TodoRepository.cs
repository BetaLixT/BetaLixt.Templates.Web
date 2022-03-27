using BetaLixT.Templates.Web.Standard.Data.Contexts;

namespace BetaLixT.Templates.Web.Standard.Data.Repositories
{
    public class TodoRepository
    {
        private readonly DatabaseContext _context;
        
        public TodoRepository(DatabaseContext context)
        {
            this._context = context;
        } 
    }
}