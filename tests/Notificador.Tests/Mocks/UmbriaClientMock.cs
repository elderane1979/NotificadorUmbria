using Moq;
using Notificador.Contracts.Interfaces;
using Notificador.Tests.FakeData.Infrastructure;

namespace Notificador.Tests.Mocks
{
    internal class UmbriaClientMock :Moq.Mock<IUmbriaClient>
    {
        public UmbriaClientMock() {
            SetupPostCredentialsAndGetHtmlAsync();
        }

        private void SetupPostCredentialsAndGetHtmlAsync()
        {
            // Configura el mock para devolver una respuesta HTML simulada para un usuario existente con contraseña VACIA
            this.Setup(m => m.PostCredentialsAndGetHtmlAsync(It.IsAny<string>(),
                                                             It.Is<string>(user => user == FakeDataUmbriaClient.FAKE_USER_REAL),
                                                             It.Is<string>(pass => string.IsNullOrEmpty(pass))))
                .ReturnsAsync(FakeDataUmbriaClient.FAKE_HTML_EMPTY_RESPONSE);

            // Configura el mock para devolver una respuesta HTML simulada para un usuario existente con contraseña correcta
            this.Setup(m => m.PostCredentialsAndGetHtmlAsync(It.IsAny<string>(),
                                                             It.Is<string>(user => user == FakeDataUmbriaClient.FAKE_USER_REAL),
                                                             It.Is<string>(pass => pass == FakeDataUmbriaClient.FAKE_PASSWORD)))
                .ReturnsAsync(FakeDataUmbriaClient.FAKE_HTML_RESPONSE);

            // Configura el mock para devolver una respuesta HTML simulada para un usuario real con contraseña incorrecta
            this.Setup(m => m.PostCredentialsAndGetHtmlAsync(It.IsAny<string>(),
                                                             It.Is<string>(user => user == FakeDataUmbriaClient.FAKE_USER_REAL),
                                                             It.Is<string>( pass => pass == FakeDataUmbriaClient.FAKE_PASSWORD_WRONG)))
                .ReturnsAsync(FakeDataUmbriaClient.FAKE_HTML_EMPTY_RESPONSE);

            // Configura el mock para devolver una respuesta HTML simulada para un usuario no existente
            this.Setup(m => m.PostCredentialsAndGetHtmlAsync(It.IsAny<string>(),
                                                             It.Is<string>(user => user != FakeDataUmbriaClient.FAKE_USER_REAL), 
                                                             It.IsAny<string>()))
                .ReturnsAsync(FakeDataUmbriaClient.FAKE_HTML_EMPTY_RESPONSE);
        }
    }
}
