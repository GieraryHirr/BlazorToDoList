using BlazorToDoList.Data;
using BlazorToDoList.Enums;
using BlazorToDoList.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BlazorToDoList.Services;

public interface IToDoService
{
    Task<string> GetCpr(string username);
    List<TodoList> GetTasks(int id);
    Task<string> AddCpr (string username, string cpr);
    Task AddTask (string username, string task);
}
public class ToDoService(
    ApplicationDbContext applicationDbContext,
    TodoDbContext todoDbContext)
    : IToDoService
{
    public async Task<string> GetCpr(string username)
    {
        if (!todoDbContext.Cprs.Any(c => c.User == username)) return "";
        {
            var cpr = await todoDbContext.Cprs.FirstOrDefaultAsync(c => c.User == username);
            return cpr?.CprNo == null ? "" : Md5Unhashing(cpr.CprNo);
        }
    }
        

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

        todoDbContext.Cprs.Add(cprToAdd);
        await todoDbContext.SaveChangesAsync();
        return hashedCpr;
    }

    public async Task AddTask(string username, string task)
    {
        var user = applicationDbContext.
            Users
            .FirstOrDefault(u => u.UserName == username);

        if (user == null) return;

        var taskToAdd = new TodoList
        {
            Item = task,
            User = user.UserName!
        };

        todoDbContext.TodoLists.Add(taskToAdd);
        await todoDbContext.SaveChangesAsync();
    }

    private string Md5Hashing(string valueToConvert)
    {
        if (valueToConvert == null) return string.Empty;
        var byteValue = Encoding.ASCII.GetBytes(valueToConvert);
        var hashedValue = MD5.Create().ComputeHash(byteValue);
        return Convert.ToBase64String(hashedValue);
    }
    private string Md5Unhashing(string hashedCpr)
    {
        var byteValue = Convert.FromBase64String(hashedCpr);
        return Encoding.ASCII.GetString(byteValue);
    }
}
