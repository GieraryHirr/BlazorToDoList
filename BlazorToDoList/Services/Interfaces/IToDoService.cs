using BlazorToDoList.Models;

namespace BlazorToDoList.Services.Interfaces;
public interface IToDoService
{
    List<TodoList> GetTasks(int id);
    Task AddTask(string username, string task);
}
