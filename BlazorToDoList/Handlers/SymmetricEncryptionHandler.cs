using Microsoft.AspNetCore.DataProtection;

namespace BlazorToDoList.Handlers;
public class SymmetricEncryptionHandler(IDataProtectionProvider dataProtectionProvider)
{
    private readonly IDataProtector _dataProtector = dataProtectionProvider.CreateProtector("Password");

    public string EncryptSymmetric(string textToEncrypt) =>
        _dataProtector.Protect(textToEncrypt);

    public string DecryptSymmetric(string textToDecrypt) =>
        _dataProtector.Unprotect(textToDecrypt);
}
