using BlazorToDoList.Handlers;
using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using BlazorToDoList.Services.Interfaces;

namespace BlazorToDoList.Services;

public class ToDoService(
    IToDoRepository toDoRepository,
    SymmetricEncryptionHandler asymmetricEncryptionHandler)
    : IToDoService
{
    public async Task<List<TodoList>> GetTasks(string username)
    {
        var encryptedToDoItems = await toDoRepository.GetTasksByUsername(username);
        encryptedToDoItems.ForEach(encryptedToDoItem =>
        {
            encryptedToDoItem.Item = asymmetricEncryptionHandler.DecryptSymmetric(encryptedToDoItem.Item);
        });

        return encryptedToDoItems;
    }

    public async Task AddTask(string username, string task)
    {
        var encryptedTask = asymmetricEncryptionHandler.EncryptSymmetric(task);
        var item = new TodoList
        {
            Item = encryptedTask,
            User = username
        };

        await toDoRepository.Add(item);
    }
}
