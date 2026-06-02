using System.Collections.Generic;
using Notificador.Contracts.Models;
using Notificador.Contracts.Interfaces;

namespace Notificador.Tests.FakeData.Infrastructure
{
    public class FakeParser : IHtmlParser
    {
        private readonly IEnumerable<MensajeDto> _mensajes;
        private readonly MensajeDto _privados;

        public FakeParser(IEnumerable<MensajeDto> mensajes, MensajeDto privados)
        {
            _mensajes = mensajes;
            _privados = privados;
        }

        public IEnumerable<MensajeDto> ParseMensajes(string html)
        {
            return _mensajes;
        }

        public MensajeDto ParseMensajesPrivados(string html)
        {
            return _privados;
        }
    }
}