using BasicAuthApi.Configurations;
using BasicAuthApi.Infrastructures;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthApi.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController(ITodoService todoService) : ControllerBase
{
    private readonly ITodoService todoService = todoService;

    private string UserId => User.GetUserId();

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoDetailsDto>>> GetAllAsync() => 
        Ok(await todoService.GetTodosAsync(UserId));

    [HttpGet("{id:int}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult<TodoDetailsDto>> GetAsync([FromRoute] int id) =>
        Ok(await todoService.GetTodoAsync(id, UserId));

    [HttpPost]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult<TodoDetailsDto>> AddAsync([FromBody] CreateTodoDto dto) => 
        Ok(await todoService.AddTodoAsync(UserId, dto));

    [HttpPost("list")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult<TodoDetailsDto>> AddListAsync([FromBody] IEnumerable<CreateTodoDto> dtos) => Ok(await todoService.AddTodosAsync(UserId, dtos));

    [HttpPut("{id:int}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateTodoDto dto)
    {
        await todoService.UpdateTodoAsync(id, UserId, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await todoService.DeleteTodoAsync(id, UserId);
        return NoContent();
    }

    [HttpGet("admin/all")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TodoDetailsDto>>> AdminGetAllAsync() =>
        Ok(await todoService.GetTodosAsync());

    [HttpDelete("admin/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task AdminDeleteAsync(int id) =>
        await todoService.DeleteTodoAsync(id);
}
