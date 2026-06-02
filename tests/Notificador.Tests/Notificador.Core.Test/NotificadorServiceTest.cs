using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Notificador.Contracts.Interfaces;
using Notificador.Contracts.Models;
using Notificador.Core.DomainMapper;
using Notificador.Core.Interfaces;
using Notificador.Core.Services;
using Notificador.Tests.FakeData.Infrastructure;
using Notificador.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Notificador.Tests.Notificador.Core.Test
{
    public class NotificadorServiceTest
    {
        private readonly INotificadorService _service;

        public NotificadorServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainMappingProfile());
            }, loggerFactory: NullLoggerFactory.Instance);

            var mapper = config.CreateMapper();
            _service = new NotificadorService(MockFactory.GetMock<IUmbriaClient>(),
                                              MockFactory.GetMock<IHtmlParser>(),
                                              MockFactory.GetMock<ISettingsService>(),
                                              MockFactory.GetMock<ICryptoService>(),
                                              mapper);
        }

        [Fact]
        public async Task GetNovedadesAsync_FiltersBySettings_ReturnsExpected()
        {
            var settings = new FakeSettings()
            {
                ShowMensajesDirector = true,
                ShowMensajesJugador = false,
                ShowMensajesVIP = false,
                ShowMensajesTalleresDirector = false,
                ShowMensajesTalleresRedactor = false,
                ShowMensajesPrivados = true
            };

            var mensajes = new List<MensajeDto>
            {
                new MensajeDto { 
                    Hilos = 1, 
                    MensajesCount = 2, 
                    Partida = "P1", 
                    Tipo = "Director" 
                },
                new MensajeDto { 
                    Hilos = 2, 
                    MensajesCount = 3, 
                    Partida = "P2", 
                    Tipo = "Jugador"
                }
            };

            var privados = new MensajeDto { 
                Hilos = 0,
                MensajesCount = 5, 
                Partida = string.Empty, 
                Tipo = "Privados" };

           

            var result = (await _service.GetNovedadesAsync()).ToList();

            // Debe incluir solo el mensaje Director y el privado
            Assert.Equal(2, result.Count);
            Assert.Contains(result, m => m.Tipo == "Director");
            Assert.Contains(result, m => m.Tipo == "Privados");
        }
    }
}
