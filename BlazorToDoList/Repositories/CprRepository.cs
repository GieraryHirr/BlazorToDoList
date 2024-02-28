using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorToDoList.Repositories;
public class CprRepository(TodoDbContext context) : ICprRepository
{
    public async Task<Cpr?> GetCprByUsername(string username) =>
        await context.Cprs.FirstOrDefaultAsync(c => c.User == username);

    public async Task Add(Cpr cpr)
    {
        context.Cprs.Add(cpr);
        await context.SaveChangesAsync();
    }
}
