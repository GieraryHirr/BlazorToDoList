using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Handlers;
public static class Md5HashingHandler
{
    public static string Hash(string value)
    {
        var byteValue = Encoding.ASCII.GetBytes(value);
        var hashedValue = MD5.Create().ComputeHash(byteValue);
        return Convert.ToBase64String(hashedValue);
    }
    public static string DeHash(string value)
    {
        var byteValue = Convert.FromBase64String(value);
        return Encoding.ASCII.GetString(byteValue);
    }
}
