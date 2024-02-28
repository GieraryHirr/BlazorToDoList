namespace BlazorToDoList.Services.Interfaces;
public interface ICprService
{
    Task<string?> GetCprNo(string username);
    Task<string?> Add(string username, string cprNo);
}
