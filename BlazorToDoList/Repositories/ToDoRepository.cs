using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;

namespace BlazorToDoList.Repositories;
public class ToDoRepository : IToDoRepository
{
    public Task<List<TodoList>> GetTasksByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task Add(TodoList toDo)
    {
        throw new NotImplementedException();
    }
}
