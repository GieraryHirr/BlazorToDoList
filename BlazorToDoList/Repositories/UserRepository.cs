using BlazorToDoList.Data;
using BlazorToDoList.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorToDoList.Repositories;
public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<ApplicationUser?> GetUserByUsername(string username) =>
        await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
}
