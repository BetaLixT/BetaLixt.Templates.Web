using BetaLixT.Templates.Web.Standard.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using BetaLixT.Templates.Web.Standard.Api.Filters;

namespace BetaLixT.Templates.Web.Standard.Api.Controller
{
    [Route("api/todo")]
    public class TodoController: ControllerBase
    {
        TodoService _service;
        public TodoController(TodoService service)
        {
           this._service = service; 
        }

        [HttpGet]
	    [ResponseCacheFilter(CacheKey = "Todo", ExpiryMinutes = 120)]
        public async Task<IActionResult> ListTodoAsync(
            [FromQuery]int pageNumber = 0,
            [FromQuery]int countPerPage = 100)
        {   
            var list = await this._service
                .ListTodo()
                .ToListAsync(x => new {
                    Title = x.Title,
                    DueDate = x.DueDate,
                    IsDone = x.IsDone,
                }, countPerPage, pageNumber);
            return this.Ok(list);
        }
    }
}
