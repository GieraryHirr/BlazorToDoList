using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorToDoList.Repositories;
public class ToDoRepository(TodoDbContext context) : IToDoRepository
{
    public async Task<List<TodoList>> GetTasksByUsername(string username) =>
        await context.TodoLists.Where(c => c.User == username).AsNoTracking().ToListAsync();

    public async Task Add(TodoList toDo)
    {
        context.TodoLists.Add(toDo);
        await context.SaveChangesAsync();
    }
}
