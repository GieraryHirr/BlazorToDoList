using System.Security.Cryptography;
using BlazorToDoList.Helpers;

namespace BlazorToDoList.Handlers;

public class AsymmetricEncryptionHandler
{
    private readonly string _privateKey;
    private readonly string _publicKey;

    public AsymmetricEncryptionHandler()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);
        }
    }

    public string EncryptAsymmetric(string textToEncrypt) =>
        AsymetricEncryptionHelper.Encrypter(textToEncrypt, _publicKey);

    public string DecryptAsymmetric(string textToDecrypt) =>
        AsymetricEncryptionHelper.Decrypter(textToDecrypt, _privateKey);
}