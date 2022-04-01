using BetaLixT.Templates.Web.Standard.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using BetaLixT.Templates.Web.Standard.Api.Filters;
using BetaLixT.Templates.Web.Standard.Api.Models.TransferObjects;

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
        public async Task<IActionResult> ListTodoAsync(
            [FromQuery]int pageNumber = 0,
            [FromQuery]int countPerPage = 100)
        {   
            var list = await this._service
                .ListTodo()
                .ToListAsync(TodoListing.Map, countPerPage, pageNumber);
            return this.Ok(list);
        }


        [HttpGet("{id}")]
        [ResponseCacheFilter(CacheKey = "Todo", ExpiryMinutes = 120)]
        public async Task<IActionResult> GetTodoAsync(Guid id)
        {
            var todo = await this._service
                .GetTodo(id)
                .GetOrDefaultAsync(TodoSummary.Map);
            return this.Ok(todo);
        }
    }
}
