using Notificador.Contracts.Interfaces;

namespace Notificador.Tests.FakeData.Infrastructure
{
    public class FakeCrypto : ICryptoService
    {
        public string Decrypt(string cipherText, string passwordKey)
        {
            return "clave";
        }
    }
}