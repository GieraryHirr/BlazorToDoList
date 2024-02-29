using BlazorToDoList.Models;

namespace BlazorToDoList.Services.Interfaces;
public interface IToDoService
{
    Task<List<TodoList>> GetTasks(string username);
    Task AddTask(string username, string task);
}
