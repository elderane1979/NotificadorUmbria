namespace Notificador.Core.Interfaces
{
    public interface ICryptoService
    {
        string Decrypt(string cipherText, string passwordKey);
        string Encrypt(string text, string passwordKey);
    }
}