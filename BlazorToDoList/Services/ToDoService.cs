using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using BlazorToDoList.Services.Interfaces;

namespace BlazorToDoList.Services;

public class ToDoService(
    IUserRepository userRepository,
    IToDoRepository toDoRepository)
    : IToDoService
{
    public List<TodoList> GetTasks(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddTask(string username, string task)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user == null) return;

        var item = new TodoList
        {
            Item = task,
            User = user.UserName!
        };

        await toDoRepository.Add(item);
    }
}
