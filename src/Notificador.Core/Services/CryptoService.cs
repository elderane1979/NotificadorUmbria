using Notificador.Core.Interfaces;

namespace Notificador.Core.Services
{
    public class CryptoService : ICryptoService
    {
        public string Decrypt(string cipherText, string passwordKey)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            try
            {
                // EasyCrypto.AesEncryption is referenced in original project; call it if available
                // If not available in this assembly, the implementation can be swapped by DI during runtime
                return EasyCrypto.AesEncryption.DecryptWithPassword(cipherText, passwordKey);
            }
            catch
            {
                // No propagamos la excepción para mantener compatibilidad; devolver cadena vacía
                return string.Empty;
            }
        }
        public string Encrypt(string text, string passwordKey)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            try
            {
                // EasyCrypto.AesEncryption is referenced in original project; call it if available
                // If not available in this assembly, the implementation can be swapped by DI during runtime
                return EasyCrypto.AesEncryption.EncryptWithPassword(text, passwordKey);
            }
            catch
            {
                // No propagamos la excepción para mantener compatibilidad; devolver cadena vacía
                return string.Empty;
            }
        }
    }
}
