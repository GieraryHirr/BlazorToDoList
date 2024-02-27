using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Codes;
public class HashingHandler(string textToHash)
{
    private readonly byte[]? _inputBytes = Encoding.ASCII.GetBytes(textToHash);

    public string Md5Hashing()
    {
        if(_inputBytes == null) return string.Empty;
        var hashedValue = MD5.Create().ComputeHash(_inputBytes);
        return Convert.ToBase64String(hashedValue);
    }


    public string Sha256Hashing()
    {
        if(_inputBytes == null) return string.Empty;
        var hashedValue = SHA256.Create().ComputeHash(_inputBytes);
        return Convert.ToBase64String(hashedValue);
    } 

    public string Hmac256Hashing()
    {
        if(_inputBytes == null) return string.Empty;
        var hmac = new HMACSHA256();
        hmac.Key = "OskarKey"u8.ToArray(); ;
        var hashedValue = hmac.ComputeHash(_inputBytes);
        return Convert.ToBase64String(hashedValue);
    }

    public string Pbkbf2Hashing(string salt, string hashingAlgorithm)
    {
        if (_inputBytes == null) return string.Empty;
        var saltAsBytesArray =  Encoding.ASCII.GetBytes(salt);
        var hashedAlgorithm = new HashAlgorithmName(hashingAlgorithm);
        var hashedValue = Rfc2898DeriveBytes
            .Pbkdf2(_inputBytes, saltAsBytesArray, 10, hashedAlgorithm,32);
        return Convert.ToBase64String(hashedValue);
    }

    public static string BCryptHashing(string textToHash) =>
         BCrypt.Net.BCrypt.HashPassword(textToHash, 10, true);

    public static bool BCryptVerify(string textToVerify, string hashedText) =>
        BCrypt.Net.BCrypt.Verify(textToVerify, hashedText, true);
}
