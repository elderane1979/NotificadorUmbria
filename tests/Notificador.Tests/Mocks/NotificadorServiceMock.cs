using Moq;
using Notificador.Core.Interfaces;
using Notificador.Core.Models;
using System.Collections.Generic;

namespace Notificador.Tests.Mocks
{
    internal class NotificadorServiceMock : Mock<INotificadorService>
    {
        private readonly ISettingService _settings;


        #region Mensajes de ejemplo
        private readonly Core.Models.Mensaje _mensajeJugador = new Core.Models.Mensaje
        {
            Hilos = 1,
            MensajesCount = 2,
            Partida = "P_Jugador",
            Tipo = "Jugador"
        };
        private readonly Core.Models.Mensaje _mensajeDirector = new Core.Models.Mensaje
        {
            Hilos = 2,
            MensajesCount = 4,
            Partida = "P_Director",
            Tipo = "Director"
        };
        private readonly Core.Models.Mensaje _mensajeVIP = new Core.Models.Mensaje
        {
            Hilos = 1,
            MensajesCount = 2,
            Partida = "P_VIP",
            Tipo = "VIP"
        };
        private readonly Core.Models.Mensaje _mensajeTallerDirector = new Core.Models.Mensaje
        {
            Hilos = 1,
            MensajesCount = 2,
            Partida = "P_Taller1",
            Tipo = "Taller (Director)"
        };
        private readonly Core.Models.Mensaje _mensajeTallerRedactor = new Core.Models.Mensaje
        {
            Hilos = 1,
            MensajesCount = 2,
            Partida = "P_Taller2",
            Tipo = "Taller (Redactor)"
        };
        private readonly Core.Models.Mensaje _mensajePrivados = new Core.Models.Mensaje
        {
            Hilos = 0,
            MensajesCount = 2,
            Partida = string.Empty,
            Tipo = "Privados"
        };
        #endregion

        public NotificadorServiceMock()
        {
            _settings =  MockFactory.GetMock<ISettingService>(); // Ensure settings mock is initialized
            SetupGetNovedades();
        }
        public NotificadorServiceMock(ISettingService settings) 
        {
            _settings = settings;
            SetupGetNovedades();
        }

        private IEnumerable<Mensaje> GetReturnMensajes()
        {
            var mensajes = new List<Mensaje>();
            if (_settings.ShowMensajesDirector)
            {
                mensajes.Add(_mensajeDirector);
            }
            if (_settings.ShowMensajesJugador)
            {
                mensajes.Add(_mensajeJugador);
            }
            if (_settings.ShowMensajesVIP)
            {
                mensajes.Add(_mensajeVIP);
            }
            if (_settings.ShowMensajesTalleresDirector)
            {
                mensajes.Add(_mensajeTallerDirector);
            }
            if (_settings.ShowMensajesTalleresRedactor)
            {
                mensajes.Add(_mensajeTallerRedactor);
            }
            if (_settings.ShowMensajesPrivados)
            {
                mensajes.Add(_mensajePrivados);
            }
            return mensajes;
        }

        public void SetupGetNovedades()
        {
            // Configuración por defecto para GetNovedadesAsync, puede ser sobreescrita en cada test
            this.Setup(n => n.GetNovedadesAsync())
                .ReturnsAsync(GetReturnMensajes());
        }
    }
}
