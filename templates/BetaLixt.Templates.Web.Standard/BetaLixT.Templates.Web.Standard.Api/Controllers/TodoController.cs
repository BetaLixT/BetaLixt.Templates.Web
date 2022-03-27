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
            this._service.ListTodo().GetAsyncEnumerator().Take(10);
            return this.Ok("");
        }
    }
}