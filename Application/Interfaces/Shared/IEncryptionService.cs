namespace Application.Interfaces.Shared
{
    public interface IEncryptionService
    {
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
        string EncryptString(string value);
        string DecryptString(string value);
    }
}
