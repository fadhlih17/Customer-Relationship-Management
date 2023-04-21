using CustomerApi.Entities;

namespace CustomerApi.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>?> GetTodosAsync();
    Todo GetTodoById(int id);
}