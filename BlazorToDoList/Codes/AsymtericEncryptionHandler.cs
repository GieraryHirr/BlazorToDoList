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

    public string EncryptAsymteric(string textToEncrypt) =>
        AsymetricEncrypter.Encrypter(textToEncrypt, _publicKey);

    public string DencryptAsymteric(string textToDecrypt) =>
        AsymetricEncrypter.Decrypter(textToDecrypt, _privateKey);
}