using BlazorToDoList.Data;

namespace BlazorToDoList.Repositories.Interfaces;
public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByUsername(string username);
}
