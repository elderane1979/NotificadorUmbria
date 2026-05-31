namespace Notificador.Infrastructure.Services
{
    public interface ICryptoService
    {
        string Decrypt(string cipherText, string passwordKey);
    }
}