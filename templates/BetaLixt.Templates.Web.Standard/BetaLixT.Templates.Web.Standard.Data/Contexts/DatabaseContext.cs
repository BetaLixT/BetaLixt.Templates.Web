using BetaLixT.Templates.Web.Standard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetaLixT.Templates.Web.Standard.Data.Contexts
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; } 
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) {}
    }
}







