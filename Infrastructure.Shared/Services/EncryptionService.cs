using Application.Interfaces.Shared;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Shared.Services
{
    class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider protector)
        {
            _protector = protector.CreateProtector(GetType().FullName);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string EncryptString(string value)
        {
            return _protector.Protect(value);
        }

        public string DecryptString(string value)
        {
            return _protector.Unprotect(value);
        }
    }
}
