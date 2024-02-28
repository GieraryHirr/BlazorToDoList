
using Microsoft.AspNetCore.DataProtection;

namespace BlazorToDoList.Codes;
public class EncryptionHandler
{
    private readonly IDataProtector _dataProtector;

    public EncryptionHandler(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector("OskarProtector");
    }

    public string EncryptSymetrisk(string textToEncrypt) => 
        _dataProtector.Protect(textToEncrypt);

    public string DecryptSymetrisk(string textToDecrypt) =>
        _dataProtector.Unprotect(textToDecrypt);
}
