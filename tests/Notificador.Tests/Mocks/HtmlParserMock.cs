using Moq;
using Notificador.Contracts.Interfaces;
using Notificador.Contracts.Models;
using Notificador.Tests.FakeData.Infrastructure;
using System.Collections.Generic;

namespace Notificador.Tests.Mocks
{
    internal class HtmlParserMock : Moq.Mock<IHtmlParser>
    {
        public HtmlParserMock()
        {
            SetupParseMensajes();
            SetupParseMensajesPrivados();
        }

        private void SetupParseMensajes()
        {
            var mensajes = new List<MensajeDto>
            {
                new MensajeDto { Hilos = 1, MensajesCount = 2, Partida = "P_Director", Tipo = "Director" },
                new MensajeDto { Hilos = 2, MensajesCount = 3, Partida = "P_Jugador", Tipo = "Jugador" },
                new MensajeDto { Hilos = 1, MensajesCount = 4, Partida = "P_VIP", Tipo = "VIP" },
                new MensajeDto { Hilos = 2, MensajesCount = 3, Partida = "P_TallerDir", Tipo = "Taller (Director)" },
                new MensajeDto { Hilos = 1, MensajesCount = 2, Partida = "P_TallerRed", Tipo = "Taller (Redactor)" }
            };

            this.Setup(m => m.ParseMensajes(It.Is<string>(html => html == FakeDataUmbriaClient.FAKE_HTML_RESPONSE)))
                             .Returns(mensajes);

            this.Setup(m => m.ParseMensajes(It.Is<string>(html => html == FakeDataUmbriaClient.FAKE_HTML_EMPTY_RESPONSE)))
                             .Returns(new List<MensajeDto>());
        }

        private void SetupParseMensajesPrivados()
        {
            var privados = new MensajeDto { Hilos = 0, MensajesCount = 5, Partida = string.Empty, Tipo = "Privados" };

            this.Setup(m => m.ParseMensajesPrivados(It.Is<string>(html => html == FakeDataUmbriaClient.FAKE_HTML_RESPONSE)))
                             .Returns(privados);
            this.Setup(m => m.ParseMensajesPrivados(It.Is<string>(html => html == FakeDataUmbriaClient.FAKE_HTML_EMPTY_RESPONSE)))
                             .Returns((MensajeDto)null);
        }
    }
}
