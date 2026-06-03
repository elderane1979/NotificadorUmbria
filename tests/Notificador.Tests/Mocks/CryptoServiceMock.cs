using Moq;
using Notificador.Core.Interfaces;
using Notificador.Tests.FakeData.Infrastructure;

namespace Notificador.Tests.Mocks
{
    internal class CryptoServiceMock : Moq.Mock<ICryptoService>
    {
        public CryptoServiceMock()
        {
            SetupDecrypt();
            SetupEncrypt();
        }

        private void SetupDecrypt()
        {
            // Por defecto devolvemos una clave desencriptada válida
            this.Setup(m => m.Decrypt(It.Is<string>(key => key == FakeDataUmbriaClient.FAKE_PASSWORD_EXCEPTION),
                                      It.IsAny<string>()))
                             .Throws(new System.Exception("decrypt failed"));

            // Por defecto devolvemos una clave desencriptada válida
            this.Setup(m => m.Decrypt(It.Is<string>(key => key == FakeDataCrypto.FAKE_ENCRYPTED_PASSWORD),
                                      It.IsAny<string>()))
                             .Returns(FakeDataUmbriaClient.FAKE_PASSWORD);
        }

        private void SetupEncrypt()
        {
            this.Setup(m => m.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns((string text, string key) => FakeData.Infrastructure.FakeDataUmbriaClient.FAKE_PASSWORD);
        }
    }
}
