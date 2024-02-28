using BlazorToDoList.Models;

namespace BlazorToDoList.Repositories.Interfaces;
public interface ICprRepository
{
    Task<Cpr?> GetCprByUsername(string username);
    Task Add(Cpr cpr);
}
