using BetaLixT.Templates.Web.Standard.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BetaLixT.Templates.Web.Standard.Api.Controller
{
    public class TodoController: ControllerBase
    {
        TodoService _service;
        public TodoController(TodoService service)
        {
           this._service = service; 
        }

        [HttpGet]
        public async Task<IActionResult> ListTodoAsync(
            [FromQuery]int pageNumber,
            [FromQuery]int countPerPage)
        {
            
            var list = await this._service.ListTodo()
                .Skip(pageNumber * countPerPage)
                .Take(countPerPage)
                .ToListAsync();
            return this.Ok(list);
        }
    }
}