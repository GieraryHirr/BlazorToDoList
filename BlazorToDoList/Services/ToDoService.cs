using BlazorToDoList.Data;
using BlazorToDoList.Enums;
using BlazorToDoList.Models;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Services;

public interface IToDoService
{
    string GetCpr(string username);
    List<TodoList> GetTasks(int id);
    Task<string> AddCpr (string username, string cpr);
    Task AddTask (string username, string task);
}
public class ToDoService : IToDoService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly TodoDbContext _todoDbContext;

    public ToDoService(ApplicationDbContext applicationDbContext, 
        TodoDbContext todoDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _todoDbContext = todoDbContext;
    }
    public string GetCpr(string username) =>
        _todoDbContext.Cprs.Any(c => c.User == username) ? 
            _todoDbContext.Cprs.FirstOrDefault(c => c.User == username)?.CprNo : "";

    public List<TodoList> GetTasks(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> AddCpr(string username, string cpr)
    {
        var hashedCpr = Md5Hashing(cpr);

        var cprToAdd = new Cpr
        {
            CprNo = hashedCpr,
            User = username
        };

        _todoDbContext.Cprs.Add(cprToAdd);
        await _todoDbContext.SaveChangesAsync();
        return hashedCpr;
    }

    public async Task AddTask(string username, string task)
    {
        var user = _applicationDbContext.
            Users
            .FirstOrDefault(u => u.UserName == username);

        if (user == null) return;


        var taskToAdd = new TodoList
        {
            Item = task,
            User = user.UserName!
        };

        _todoDbContext.TodoLists.Add(taskToAdd);
        await _todoDbContext.SaveChangesAsync();
    }

    private string Md5Hashing(string valueToConvert)
    {
        if (valueToConvert == null) return string.Empty;
        var byteValue = Encoding.ASCII.GetBytes(valueToConvert);
        var hashedValue = MD5.Create().ComputeHash(byteValue);
        return Convert.ToBase64String(hashedValue);
    }
}
