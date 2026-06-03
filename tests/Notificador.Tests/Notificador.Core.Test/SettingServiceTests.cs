using Moq;
using Notificador.Core.Services;
using Notificador.Contracts.Interfaces;
using Notificador.Tests.Mocks;
using Xunit;
using TestMockFactory = Notificador.Tests.Mocks.MockFactory;

namespace Notificador.Tests.Notificador.Core.Test
{
    public class SettingServiceTests
    {
        private SettingService GetService(ISettingsProvider provider = null)
        {
            var p = provider ?? TestMockFactory.GetMock<ISettingsProvider>();
            return new SettingService(p);
        }

        [Fact]
        public void Url_Getter_ForwardsToProvider_UsingMockFactory()
        {
            // Arrange
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            var expected = "https://factory.example";
            mock.Object.Url = expected; // assign property on mock object (no Setup in test)
            var service = GetService(mock.Object);

            // Act
            var actual = service.Url;

            // Assert
            Assert.Equal(expected, actual);
            mock.VerifyGet(p => p.Url, Times.Once);
        }

        [Fact]
        public void Url_Setter_ForwardsToProvider_UsingMockFactory()
        {
            // Arrange
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.SetupAllProperties(); // ensure backing storage (factory already does this by default but safe)
            var service = GetService(mock.Object);
            var newValue = "https://set.example";

            // Act
            service.Url = newValue;

            // Assert
            mock.VerifySet(p => p.Url = newValue, Times.Once);
            Assert.Equal(newValue, mock.Object.Url);
        }

        [Fact]
        public void Usuario_GetterAndSetter_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.Object.Usuario = "usuarioX";
            var service = GetService(mock.Object);

            Assert.Equal("usuarioX", service.Usuario);
            mock.VerifyGet(p => p.Usuario, Times.Once);

            // Setter
            service.Usuario = "nuevoUsuario";
            mock.VerifySet(p => p.Usuario = "nuevoUsuario", Times.Once);
            Assert.Equal("nuevoUsuario", mock.Object.Usuario);
        }

        [Fact]
        public void ClaveEncriptada_GetterAndSetter_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.Object.ClaveEncriptada = "claveInicial";
            var service = GetService(mock.Object);

            Assert.Equal("claveInicial", service.ClaveEncriptada);
            mock.VerifyGet(p => p.ClaveEncriptada, Times.Once);

            service.ClaveEncriptada = "claveNueva";
            mock.VerifySet(p => p.ClaveEncriptada = "claveNueva", Times.Once);
            Assert.Equal("claveNueva", mock.Object.ClaveEncriptada);
        }

        [Fact]
        public void ShowMensajesDirector_GetterAndSetter_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.Object.ShowMensajesDirector = true;
            var service = GetService(mock.Object);

            Assert.True(service.ShowMensajesDirector);
            mock.VerifyGet(p => p.ShowMensajesDirector, Times.Once);

            service.ShowMensajesDirector = false;
            mock.VerifySet(p => p.ShowMensajesDirector = false, Times.Once);
            Assert.False(mock.Object.ShowMensajesDirector);
        }

        [Fact]
        public void IdMensajesDirector_GetterAndSetter_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.Object.IdMensajesDirector = "dir-123";
            var service = GetService(mock.Object);

            Assert.Equal("dir-123", service.IdMensajesDirector);
            mock.VerifyGet(p => p.IdMensajesDirector, Times.Once);

            service.IdMensajesDirector = "dir-999";
            mock.VerifySet(p => p.IdMensajesDirector = "dir-999", Times.Once);
            Assert.Equal("dir-999", mock.Object.IdMensajesDirector);
        }

        [Fact]
        public void Espera_GetterAndSetter_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            mock.Object.Espera = 10;
            var service = GetService(mock.Object);

            Assert.Equal(10, service.Espera);
            mock.VerifyGet(p => p.Espera, Times.Once);

            service.Espera = 25;
            mock.VerifySet(p => p.Espera = 25, Times.Once);
            Assert.Equal(25, mock.Object.Espera);
        }

        [Fact]
        public void Save_CallsProviderSave_UsingMockFactory()
        {
            var mock = TestMockFactory.GetMockInstance<ISettingsProvider>();
            var service = GetService(mock.Object);

            service.Save();

            mock.Verify(p => p.Save(), Times.Once);
        }
    }
}
