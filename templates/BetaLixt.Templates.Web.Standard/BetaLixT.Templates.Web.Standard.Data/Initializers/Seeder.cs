using BetaLixT.Templates.Web.Standard.Utility.Startup;
using BetaLixT.Templates.Web.Standard.Data.Contexts;
using BetaLixT.Templates.Web.Standard.Data.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace BetaLixT.Templates.Web.Standard.Data.Initializers
{
    public class Seeder : IInitializer
    {
        private readonly DatabaseContext _context;

        public bool IsRequired { get; } = false;

        public Seeder(IDbContextFactory<DatabaseContext> contextFac)
        {
            this._context = contextFac.CreateDbContext();
        }

        public void Initialize()
        {
            if(!this._context.Todos.Any())
            {
                var now = DateTimeOffset.UtcNow;
                var todos = new List<Todo>
                {
                    new Todo {
                        Id = Guid.Parse("a06f69a4-c469-4d41-82ef-7dfc7b34348f"),
                        Title = "Buy Milk",
                        Description = "Need to buy one litter of Milk",
                        DueDate = DateTimeOffset.UtcNow.AddDays(1),
                        IsDone = false,
                        CreatedOn = now,
                        UpdatedOn = now,
                    },
                    new Todo {
                        Id = Guid.NewGuid(),
                        Title = "Fix Networking",
                        Description = "Fix slow networking issue",
                        DueDate = DateTimeOffset.UtcNow.AddDays(5),
                        IsDone = false,
                        CreatedOn = now,
                        UpdatedOn = now,
                    },
                     new Todo {
                        Id = Guid.NewGuid(),
                        Title = "Document Project",
                        Description = "Complete the project documentation",
                        DueDate = DateTimeOffset.UtcNow.AddDays(20),
                        IsDone = false,
                        CreatedOn = now,
                        UpdatedOn = now,
                    }
                };
                this._context.Todos.AddRange(todos);
                this._context.SaveChanges();
            }
        }   
    }
}