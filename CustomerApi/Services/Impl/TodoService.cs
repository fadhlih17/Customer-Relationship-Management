using System.Net;
using CustomerApi.Entities;
using CustomerApi.Exceptions;
using Newtonsoft.Json;

namespace CustomerApi.Services.Impl;

public class TodoService : ITodoService
{
    private readonly HttpClient _httpClient;

    public TodoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Todo>?> GetTodosAsync()
    {
        var responseTodo = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
        responseTodo.EnsureSuccessStatusCode();
        var content = await responseTodo.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<Todo>>(content);
    }

    public Todo GetTodoById(int id)
    {
        var responseTodo = _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}").Result;
        if (!responseTodo.IsSuccessStatusCode)
        {
            if (responseTodo.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Todo Not Found");
            }
            else
            {
                throw new Exception("Error Calling Api, Status Code: " + responseTodo.StatusCode);
            }
        }
        var content = responseTodo.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var todo = JsonConvert.DeserializeObject<Todo>(content);
        return todo;
    }
}