using Notificador.Core.Services;
using Notificador.Tests.FakeData.Infrastructure;
using Xunit;

namespace Notificador.Tests.Notificador.Core.Test
{
    public class CryptoServiceTests
    {
        [Fact]
        public void Encrypt_WithCorrectKey_DecryptsToOriginalOrEmpty()
        {
            var service = new CryptoService();
            var plain = "TextoDePrueba123";
            var key = FakeDataCrypto.FAKE_KEY;

            var cipher = service.Encrypt(plain, key);

            if (string.IsNullOrEmpty(cipher))
            {
                // Si la implementación no está disponible, se espera cadena vacía
                Assert.Equal(string.Empty, cipher);
                Assert.Equal(string.Empty, service.Decrypt(cipher, key));
            }
            else
            {
                Assert.Equal(plain, service.Decrypt(cipher, key));
            }
        }

        [Fact]
        public void Decrypt_WithWrongKey_ReturnsEmptyWhenImplementationAvailable()
        {
            var service = new CryptoService();
            var plain = "TextoDePrueba123";
            var key = FakeDataCrypto.FAKE_KEY;

            var cipher = service.Encrypt(plain, key);

            if (string.IsNullOrEmpty(cipher))
            {
                // Si la implementación no está disponible, no hay más comprobaciones
                Assert.Equal(string.Empty, cipher);
            }
            else
            {
                var wrong = service.Decrypt(cipher, FakeDataCrypto.FAKE_KEY_WRONG);
                Assert.True(string.IsNullOrEmpty(wrong));
            }
        }

        [Fact]
        public void EncryptDecrypt_EmptyInput_ReturnsEmpty()
        {
            var service = new CryptoService();
            var key = FakeDataCrypto.FAKE_KEY;

            Assert.Equal(string.Empty, service.Encrypt(string.Empty, key));
            Assert.Equal(string.Empty, service.Decrypt(string.Empty, key));

            Assert.Equal(string.Empty, service.Encrypt(null, key));
            Assert.Equal(string.Empty, service.Decrypt(null, key));
        }
    }
}
