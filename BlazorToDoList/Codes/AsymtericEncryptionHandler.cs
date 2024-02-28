using System.Security.Cryptography;
using System.Text;

namespace BlazorToDoList.Codes;

public class AsymtericEncryptionHandler
{
    private string _privateKey;
    private string _publicKey;

    public AsymtericEncryptionHandler()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);
        }
    }

    public string EncryptAsymteric(string textToEncrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_publicKey);
            var data = Encoding.UTF8.GetBytes(textToEncrypt);
            var encryptedData = rsa.Encrypt(data, true);
            return Convert.ToBase64String(encryptedData);
        }
    }

    public string DencryptAsymteric(string textToDecrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_privateKey);
            var data = Convert.FromBase64String(textToDecrypt);
            var decryptedData = rsa.Decrypt(data, true);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}