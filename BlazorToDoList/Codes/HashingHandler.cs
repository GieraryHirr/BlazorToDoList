using System.Security.Cryptography;
using System.Text;
using BlazorToDoList.Enums;

namespace BlazorToDoList.Codes;

public class HashingHandler(string textToHash)
{
    private readonly byte[]? _inputBytes = Encoding.ASCII.GetBytes(textToHash);

    public string Md5Hashing(HashFormat hashFormat)
    {
        if (_inputBytes == null) return string.Empty;
        var hashedValue = MD5.Create().ComputeHash(_inputBytes);
        return ConvertToHashFormat(hashedValue, hashFormat);
    }

    public string Sha256Hashing(HashFormat hashFormat)
    {
        if(_inputBytes == null) return string.Empty;
        var hashedValue = SHA256.Create().ComputeHash(_inputBytes);
        return ConvertToHashFormat(hashedValue, hashFormat);
    } 

    public string Hmac256Hashing(HashFormat hashFormat)
    {
        if(_inputBytes == null) return string.Empty;
        var hmac = new HMACSHA256();
        hmac.Key = "OskarKey"u8.ToArray(); ;
        var hashedValue = hmac.ComputeHash(_inputBytes);
        return ConvertToHashFormat(hashedValue, hashFormat);
    }

    public string Pbkbf2Hashing(string salt, string hashingAlgorithm, HashFormat hashFormat)
    {
        if (_inputBytes == null) return string.Empty;
        var saltAsBytesArray =  Encoding.ASCII.GetBytes(salt);
        var hashedAlgorithm = new HashAlgorithmName(hashingAlgorithm);
        var hashedValue = Rfc2898DeriveBytes
            .Pbkdf2(_inputBytes, saltAsBytesArray, 10, hashedAlgorithm,32);
        return ConvertToHashFormat(hashedValue, hashFormat);
    }

    public static string BCryptHashing(string textToHash) =>
         BCrypt.Net.BCrypt.HashPassword(textToHash, 10, true);

    public static bool BCryptVerify(string textToVerify, string hashedText) =>
        BCrypt.Net.BCrypt.Verify(textToVerify, hashedText, true);

    private static string ConvertToHashFormat(byte[] hashedValue, HashFormat hashFormat) =>
        hashFormat switch
        {
            HashFormat.Base64 => Convert.ToBase64String(hashedValue),
            HashFormat.Utf => Encoding.UTF8.GetString(hashedValue),
            HashFormat.Hex => BitConverter.ToString(hashedValue).Replace("-", string.Empty),
            HashFormat.ByteString => string.Join(" ", hashedValue.Select(b => b.ToString())),
            HashFormat.ByteArray => string.Join(",", hashedValue),
            _ => throw new ArgumentException("Invalid output format specified.", nameof(hashFormat))
        };
}
