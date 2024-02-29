using BlazorToDoList.Enums;
using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using BlazorToDoList.Services.Interfaces;
using BlazorToDoList.Helpers;

namespace BlazorToDoList.Services;

public class CprService(ICprRepository cprRepository) : ICprService
{
    public async Task<string?> GetCprNo(string username)
    {
        var cpr = await cprRepository.GetCprByUsername(username);
        return cpr?.CprNo;
    }

    public async Task<string?> Add(string username, string cprNo)
    {
        var newCprNoHashed = HashingHelper.Md5Hashing(cprNo, HashFormat.Base64);

        var cpr = await cprRepository.GetCprByUsername(username);
        if (cpr != null )
            return cpr.CprNo == newCprNoHashed ? cpr.CprNo : null;

        var newCpr = new Cpr
        {
            CprNo = newCprNoHashed,
            User = username
        };

        await cprRepository.Add(newCpr);
        return newCpr.CprNo;
    }
}
