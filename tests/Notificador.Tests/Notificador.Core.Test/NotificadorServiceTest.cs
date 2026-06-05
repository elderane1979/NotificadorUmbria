using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Notificador.Contracts.Interfaces;
using Notificador.Core.DomainMapper;
using Notificador.Core.Interfaces;
using Notificador.Core.Services;
using Notificador.Tests.FakeData.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Notificador.Tests.Notificador.Core.Test
{
    public class NotificadorServiceTest
    {
        private static INotificadorService CreateService(IUmbriaClient umbriaClient = null, 
                                                  IHtmlParser htmlParser = null,
                                                  ISettingService settingService = null, 
                                                  ICryptoService cryptoService = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainMappingProfile());
            }, loggerFactory: NullLoggerFactory.Instance);

            var mapper = config.CreateMapper();

            // Obtener mocks por defecto desde la fábrica
            if (umbriaClient == null)
                umbriaClient = Mocks.MockFactory.GetMock<IUmbriaClient>();
            if (htmlParser == null)
                htmlParser = Mocks.MockFactory.GetMock<IHtmlParser>();
            if(settingService == null)
                settingService = Mocks.MockFactory.GetMock<ISettingService>();
            if(cryptoService == null)
                cryptoService = Mocks.MockFactory.GetMock<ICryptoService>();

            return new NotificadorService(umbriaClient, htmlParser, settingService, cryptoService, mapper);
        }

        [Fact]
        public async Task GetNovedadesAsync_FiltersBySettings_ReturnsExpected()
        {
            var service = CreateService();

            var result = (await service.GetNovedadesAsync()).ToList();

            // Debe incluir los mensajes según los setups por defecto: Director, Jugador y Privados
            Assert.Contains(result, m => m.Tipo == "Director");
            Assert.Contains(result, m => m.Tipo == "Jugador");
            Assert.Contains(result, m => m.Tipo == "Privados");
        }

        [Fact]
        public async Task GetNovedadesAsync_WhenCryptoDecryptThrows_UsesEmptyClave()
        {
            //Arrange
            var settingProvider = Mocks.MockFactory.GetMock<ISettingService>();
            settingProvider.ClaveEncriptada = FakeDataUmbriaClient.FAKE_PASSWORD_EXCEPTION;

            // Prepare mocks and override crypto to throw
            var service = CreateService(settingService: settingProvider);
            
            //Act
            var result = await service.GetNovedadesAsync();

            //Assert
            // Should still return mensajes parsed (defaults)
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetNovedadesAsync_WhenNoFlagsSet_ReturnsOnlyPrivadosIfEnabled()
        {
            //Arrange
            // Prepare mocks and override settings to disable most flags
            var settingsMock = Mocks.MockFactory.GetMock<ISettingService>();
            if (settingsMock != null)
            {
                settingsMock.ShowMensajesDirector= false;
                settingsMock.ShowMensajesJugador = false;
                settingsMock.ShowMensajesVIP = false;
                settingsMock.ShowMensajesTalleresDirector = false;
                settingsMock.ShowMensajesTalleresRedactor = false;
                settingsMock.ShowMensajesPrivados = true;
            }
            var service = CreateService(settingService: settingsMock);

            //Act
            var result = (await service.GetNovedadesAsync()).ToList();

            //Assert
            Assert.All(result, m => Assert.Equal("Privados", m.Tipo) );
        }

        [Theory]
        [InlineData(true, false, false, false, false, false)] // solo Director
        [InlineData(false, true, false, false, false, false)] // solo Jugador
        [InlineData(false, true, false, false, false, true)]  // Jugador + Privados
        [InlineData(true, true, false, false, false, false)]  // Director + Jugador
        [InlineData(false, false, true, true, true, false)]   // VIP + Talleres
        public async Task GetNovedadesAsync_VariousSettings_CombinationsBehaveAsExpected(bool director, bool jugador, bool vip, bool tallerDirector, bool tallerRedactor, bool privados)
        {
            var settings = Mocks.MockFactory.GetMock<ISettingService>();

            // Aplicar las propiedades directamente sobre el objeto devuelto por la fábrica
            settings.ShowMensajesDirector = director;
            settings.ShowMensajesJugador = jugador;
            settings.ShowMensajesVIP = vip;
            settings.ShowMensajesTalleresDirector = tallerDirector;
            settings.ShowMensajesTalleresRedactor = tallerRedactor;
            settings.ShowMensajesPrivados = privados;

            var service = CreateService(settingService: settings);

            var result = (await service.GetNovedadesAsync()).ToList();

            // Comprobaciones para Director/Jugador/Privados (parser por defecto devuelve Director, Jugador y Privados)
            if (director)
            {
                Assert.Contains(result, m => m.Tipo == "Director");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "Director");
            }

            if (jugador)
            {
                Assert.Contains(result, m => m.Tipo == "Jugador");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "Jugador");
            }

            if (vip)
            {
                Assert.Contains(result, m => m.Tipo == "VIP");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "VIP");
            }

            if (tallerDirector)
            {
                Assert.Contains(result, m => m.Tipo == "Taller (Director)");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "Taller (Director)");
            }

            if (tallerRedactor)
            {
                Assert.Contains(result, m => m.Tipo == "Taller (Redactor)");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "Taller (Redactor)");
            }
            if (privados)
            {
                Assert.Contains(result, m => m.Tipo == "Privados");
            }
            else
            {
                Assert.DoesNotContain(result, m => m.Tipo == "Privados");
            }
        }
    }
}
