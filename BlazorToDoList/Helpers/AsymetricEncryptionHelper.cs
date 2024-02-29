using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Helpers;

public class AsymetricEncryptionHelper
{
    public static string Encrypter(string textToEncrypt, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            var data = Encoding.UTF8.GetBytes(textToEncrypt);
            var encryptedData = rsa.Encrypt(data, true);
            return Convert.ToBase64String(encryptedData);
        }
    }

    public static string Decrypter(string textToDecrypt, string privateKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            var data = Convert.FromBase64String(textToDecrypt);
            var decryptedData = rsa.Decrypt(data, true);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}