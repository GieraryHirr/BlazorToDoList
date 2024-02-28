using BlazorToDoList.Models;

namespace BlazorToDoList.Repositories.Interfaces;
public interface IToDoRepository
{
    Task<List<TodoList>> GetTasksByUsername(string username);
    Task Add(TodoList toDo);
}
