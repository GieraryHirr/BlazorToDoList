using BlazorToDoList.Models;
using BlazorToDoList.Repositories.Interfaces;
using BlazorToDoList.Handlers;
using BlazorToDoList.Services.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        var newCprNoHashed = Md5HashingHandler.Hash(cprNo);

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
