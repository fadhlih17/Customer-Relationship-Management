using CustomerApi.Services;
using CustomerApi.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("/api/todos")]
public class TodoController : ControllerBase
{
    private ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _todoService.GetTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoById(int id)
    {
        var todo = _todoService.GetTodoById(id);
        return Ok(todo);
    }
}