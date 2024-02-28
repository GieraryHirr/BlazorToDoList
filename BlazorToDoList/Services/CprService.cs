using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using BlazorToDoList.Handlers;
using BlazorToDoList.Services.Interfaces;

namespace BlazorToDoList.Services;

public class CprService(ICprRepository cprRepository) : ICprService
{
    public async Task<string?> GetCprNo(string username)
    {
        var cpr = await cprRepository.GetCprByUsername(username);
        return cpr?.CprNo;
    }

    public async Task<string> Add(string username, string cprNo)
    {
        var hashedCpr = Md5HashingHandler.Hash(cprNo);

        var cpr = new Cpr
        {
            CprNo = hashedCpr,
            User = username
        };

        await cprRepository.Add(cpr);
        return cpr.CprNo;
    }
}
