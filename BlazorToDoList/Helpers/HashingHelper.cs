using BlazorToDoList.Enums;
using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Helpers;
public static class HashingHelper
{
    public static string Md5Hashing(string value, HashFormat format)
    {
        var valueBytes = Encoding.ASCII.GetBytes(value);
        var hashedValue = MD5.Create().ComputeHash(valueBytes);
        return ConvertToHashFormat(hashedValue, format);
    }

    public static string Sha256Hashing(string value, HashFormat format)
    {
        var valueBytes = Encoding.ASCII.GetBytes(value);
        var hashedValue = SHA256.Create().ComputeHash(valueBytes);
        return ConvertToHashFormat(hashedValue, format);
    }

    public static string Hmac256Hashing(string value, HashFormat format)
    {
        var valueBytes = Encoding.ASCII.GetBytes(value);
        var hmac = new HMACSHA256();
        hmac.Key = "OskarKey"u8.ToArray();
        var hashedValue = hmac.ComputeHash(valueBytes);
        return ConvertToHashFormat(hashedValue, format);
    }

    public static string Pbkbf2Hashing(string value, HashFormat format, string salt, string hashingAlgorithm)
    {
        var valueBytes = Encoding.ASCII.GetBytes(value);
        var saltAsBytesArray = Encoding.ASCII.GetBytes(salt);
        var hashedAlgorithm = new HashAlgorithmName(hashingAlgorithm);
        var hashedValue = Rfc2898DeriveBytes
            .Pbkdf2(valueBytes, saltAsBytesArray, 10, hashedAlgorithm, 32);
        return ConvertToHashFormat(hashedValue, format);
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
            HashFormat.ByteArray => string.Join(", ", hashedValue),
            _ => throw new ArgumentException("Invalid output format specified.", nameof(hashFormat))
        };
}
