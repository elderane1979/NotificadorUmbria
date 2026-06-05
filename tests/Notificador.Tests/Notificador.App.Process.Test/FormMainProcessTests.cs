using Moq;
using Notificador.App.Process;
using Notificador.Contracts.Helper;
using Notificador.Core.Interfaces;
using Notificador.Core.Models;
using Notificador.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Notificador.Tests.Notificador.App.Process.Test
{
    public class FormMainProcessTests
    {
        private static FormMainProcess CreateService(ISettingService settings = null, INotificadorService notificador = null)
        {
            if (settings == null)
            {
                settings = Mocks.MockFactory.GetMock<ISettingService>();
            }

            if (notificador == null)
            {
                // Create a default mock that returns empty list to avoid network dependency
                var notificadorMopck = new NotificadorServiceMock(settings);
                notificador = notificadorMopck.Object;
            }

            return new FormMainProcess(settings, notificador);
        }

        [Fact]
        public async Task ComprobarAsync_WithMessages_ReturnsComposedMessages()
        {
            // Arrange
            var settings = Mocks.MockFactory.GetMock<ISettingService>();
            settings.Resumido = false; // request full message
            settings.ShowMensajesDirector = true;
            settings.ShowMensajesJugador = true;
            settings.ShowMensajesVIP = false;
            settings.ShowMensajesTalleresRedactor = false;
            settings.ShowMensajesTalleresDirector = false;
            settings.ShowMensajesPrivados = true;


            var service = CreateService(settings: settings);

            // Act
            var result = await service.ComprobarAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Director", result.Mensaje);
            Assert.Contains("Jugador", result.Mensaje);
            Assert.Contains("Privados", result.Mensaje);

            // Resumido should be generated as well (true passed to CrearMensaje when building MensajeResumido)
            Assert.False(string.IsNullOrEmpty(result.MensajeResumido));
        }

        [Fact]
        public async Task ComprobarAsync_WithNoMessages_ReturnsNoMensajesConstant()
        {
            // Arrange
            var settings = Mocks.MockFactory.GetMock<ISettingService>();
            settings.Resumido = true;
            settings.ShowMensajesDirector = false;
            settings.ShowMensajesJugador = false;
            settings.ShowMensajesVIP= false;
            settings.ShowMensajesTalleresRedactor = false;
            settings.ShowMensajesTalleresDirector = false;
            settings.ShowMensajesPrivados = false;


            var service = CreateService(settings);

            // Act
            var result = await service.ComprobarAsync();

            // Assert
            Assert.Equal(Constantes.NO_MENSAJES, result.Mensaje);
            Assert.Equal(Constantes.NO_MENSAJES, result.MensajeResumido);
        }

        [Fact]
        public async Task ComprobarAsync_WhenServiceThrows_ThrowsArgumentException()
        {
            // Arrange
            var settings = Mocks.MockFactory.GetMock<ISettingService>();

            var notificadorMock = new Moq.Mock<INotificadorService>();
            notificadorMock.Setup(n => n.GetNovedadesAsync()).ThrowsAsync(new Exception("boom"));

            var service = CreateService(settings, notificadorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.ComprobarAsync());
        }

        [Fact]
        public void CalcularIntervalo_ReturnsExpectedFloat()
        {
            // Arrange
            var service = CreateService();

            // Act
            var value = service.CalcularIntervalo(0, 100, 1); // (100-0)/(1*60) = 100/60

            // Assert
            Assert.InRange(value, 1.6666f, 1.6668f);
        }
    }
}
