using Moq;
using Notificador.Core.Services;
using Notificador.Contracts.Interfaces;
using Xunit;

namespace Notificador.Tests
{
    public class SettingServiceTests
    {
        [Fact]
        public void SettingService_WithValidProvider_InstanceCreatedAndUsesProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.Url).Returns("http://test");

            // Act
            var service = new SettingService(mockProvider.Object);
            var url = service.Url;

            // Assert
            Assert.NotNull(service);
            Assert.Equal("http://test", url);
            mockProvider.VerifyGet(p => p.Url, Times.Once);
        }

        [Fact]
        public void Url_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = "https://example.com";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.Url).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.Url;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.Url, Times.Once);
        }

        [Fact]
        public void Url_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "https://set.example";

            // Act
            service.Url = newValue;

            // Assert
            mockProvider.VerifySet(p => p.Url = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.Url);
        }

        [Fact]
        public void Usuario_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = "usuario1";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.Usuario).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.Usuario;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.Usuario, Times.Once);
        }

        [Fact]
        public void Usuario_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "nuevoUsuario";

            // Act
            service.Usuario = newValue;

            // Assert
            mockProvider.VerifySet(p => p.Usuario = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.Usuario);
        }

        [Fact]
        public void ClaveEncriptada_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = "clave123";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ClaveEncriptada).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.ClaveEncriptada;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ClaveEncriptada, Times.Once);
        }

        [Fact]
        public void ClaveEncriptada_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "nuevaClave";

            // Act
            service.ClaveEncriptada = newValue;

            // Assert
            mockProvider.VerifySet(p => p.ClaveEncriptada = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ClaveEncriptada);
        }

        [Fact]
        public void ShowMensajesDirector_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = true;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesDirector).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.ShowMensajesDirector;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesDirector, Times.Once);
        }

        [Fact]
        public void ShowMensajesDirector_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            // Act
            service.ShowMensajesDirector = newValue;

            // Assert
            mockProvider.VerifySet(p => p.ShowMensajesDirector = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesDirector);
        }

        [Fact]
        public void ShowMensajesJugador_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = false;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesJugador).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.ShowMensajesJugador;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesJugador, Times.Once);
        }

        [Fact]
        public void ShowMensajesJugador_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            // Act
            service.ShowMensajesJugador = newValue;

            // Assert
            mockProvider.VerifySet(p => p.ShowMensajesJugador = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesJugador);
        }

        [Fact]
        public void ShowMensajesVIP_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = true;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesVIP).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.ShowMensajesVIP;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesVIP, Times.Once);
        }

        [Fact]
        public void ShowMensajesVIP_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = false;

            // Act
            service.ShowMensajesVIP = newValue;

            // Assert
            mockProvider.VerifySet(p => p.ShowMensajesVIP = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesVIP);
        }

        [Fact]
        public void ShowMensajesTalleresDirector_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = true;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesTalleresDirector).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.ShowMensajesTalleresDirector;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesTalleresDirector, Times.Once);
        }

        [Fact]
        public void ShowMensajesTalleresDirector_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            // Act
            service.ShowMensajesTalleresDirector = newValue;

            // Assert
            mockProvider.VerifySet(p => p.ShowMensajesTalleresDirector = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesTalleresDirector);
        }

        [Fact]
        public void IdMensajesDirector_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = "director-123";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesDirector).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.IdMensajesDirector;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesDirector, Times.Once);
        }

        [Fact]
        public void IdMensajesDirector_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "dir-999";

            // Act
            service.IdMensajesDirector = newValue;

            // Assert
            mockProvider.VerifySet(p => p.IdMensajesDirector = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesDirector);
        }

        [Fact]
        public void IdMensajesJugador_Getter_ForwardsToProvider()
        {
            // Arrange
            var expected = "jugador-abc";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesJugador).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            // Act
            var actual = service.IdMensajesJugador;

            // Assert
            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesJugador, Times.Once);
        }

        [Fact]
        public void IdMensajesJugador_Setter_ForwardsToProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "jug-777";

            // Act
            service.IdMensajesJugador = newValue;

            // Assert
            mockProvider.VerifySet(p => p.IdMensajesJugador = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesJugador);
        }
        [Fact]
        public void IdMensajesVIP_Getter_ForwardsToProvider()
        {
            var expected = "vip-001";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesVIP).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.IdMensajesVIP;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesVIP, Times.Once);
        }

        [Fact]
        public void IdMensajesVIP_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "vip-999";

            service.IdMensajesVIP = newValue;

            mockProvider.VerifySet(p => p.IdMensajesVIP = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesVIP);
        }

        [Fact]
        public void IdMensajesTalleresDirector_Getter_ForwardsToProvider()
        {
            var expected = "taller-dir";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesTalleresDirector).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.IdMensajesTalleresDirector;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesTalleresDirector, Times.Once);
        }

        [Fact]
        public void IdMensajesTalleresDirector_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "taller-dir-2";

            service.IdMensajesTalleresDirector = newValue;

            mockProvider.VerifySet(p => p.IdMensajesTalleresDirector = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesTalleresDirector);
        }

        [Fact]
        public void IdMensajesTalleresRedactor_Getter_ForwardsToProvider()
        {
            var expected = "taller-red";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesTalleresRedactor).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.IdMensajesTalleresRedactor;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesTalleresRedactor, Times.Once);
        }

        [Fact]
        public void IdMensajesTalleresRedactor_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "taller-red-7";

            service.IdMensajesTalleresRedactor = newValue;

            mockProvider.VerifySet(p => p.IdMensajesTalleresRedactor = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesTalleresRedactor);
        }

        [Fact]
        public void IdMensajesPrivados_Getter_ForwardsToProvider()
        {
            var expected = "priv-1";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.IdMensajesPrivados).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.IdMensajesPrivados;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.IdMensajesPrivados, Times.Once);
        }

        [Fact]
        public void IdMensajesPrivados_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "priv-42";

            service.IdMensajesPrivados = newValue;

            mockProvider.VerifySet(p => p.IdMensajesPrivados = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IdMensajesPrivados);
        }

        [Fact]
        public void TagMensajesPrivados_Getter_ForwardsToProvider()
        {
            var expected = "#privados";
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.TagMensajesPrivados).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.TagMensajesPrivados;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.TagMensajesPrivados, Times.Once);
        }

        [Fact]
        public void TagMensajesPrivados_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "#nuevo";

            service.TagMensajesPrivados = newValue;

            mockProvider.VerifySet(p => p.TagMensajesPrivados = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.TagMensajesPrivados);
        }

        [Fact]
        public void ShowMensajesTalleresRedactor_Getter_ForwardsToProvider()
        {
            var expected = true;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesTalleresRedactor).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.ShowMensajesTalleresRedactor;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesTalleresRedactor, Times.Once);
        }

        [Fact]
        public void ShowMensajesTalleresRedactor_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = false;

            service.ShowMensajesTalleresRedactor = newValue;

            mockProvider.VerifySet(p => p.ShowMensajesTalleresRedactor = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesTalleresRedactor);
        }

        [Fact]
        public void ShowMensajesPrivados_Getter_ForwardsToProvider()
        {
            var expected = false;
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupGet(p => p.ShowMensajesPrivados).Returns(expected);
            var service = new SettingService(mockProvider.Object);

            var actual = service.ShowMensajesPrivados;

            Assert.Equal(expected, actual);
            mockProvider.VerifyGet(p => p.ShowMensajesPrivados, Times.Once);
        }

        [Fact]
        public void ShowMensajesPrivados_Setter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            service.ShowMensajesPrivados = newValue;

            mockProvider.VerifySet(p => p.ShowMensajesPrivados = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowMensajesPrivados);
        }

        [Fact]
        public void Espera_GetterAndSetter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = 30;

            service.Espera = newValue;

            mockProvider.VerifySet(p => p.Espera = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.Espera);
        }

        [Fact]
        public void Resumido_GetterAndSetter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            service.Resumido = newValue;

            mockProvider.VerifySet(p => p.Resumido = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.Resumido);
        }

        [Fact]
        public void ShowNotificacionSinMensajes_GetterAndSetter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            service.ShowNotificacionSinMensajes = newValue;

            mockProvider.VerifySet(p => p.ShowNotificacionSinMensajes = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.ShowNotificacionSinMensajes);
        }

        [Fact]
        public void Browser_GetterAndSetter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = "Edge";

            service.Browser = newValue;

            mockProvider.VerifySet(p => p.Browser = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.Browser);
        }

        [Fact]
        public void IniciarMinimizado_GetterAndSetter_ForwardsToProvider()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            mockProvider.SetupAllProperties();
            var service = new SettingService(mockProvider.Object);
            var newValue = true;

            service.IniciarMinimizado = newValue;

            mockProvider.VerifySet(p => p.IniciarMinimizado = newValue, Times.Once);
            Assert.Equal(newValue, mockProvider.Object.IniciarMinimizado);
        }

        [Fact]
        public void Save_CallsProviderSave()
        {
            var mockProvider = new Mock<ISettingsProvider>();
            var service = new SettingService(mockProvider.Object);

            service.Save();

            mockProvider.Verify(p => p.Save(), Times.Once);
        }

    }
}
