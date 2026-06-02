using System.Collections.Generic;
using Notificador.Contracts.Models;

namespace Notificador.Contracts.Interfaces
{
    public interface IHtmlParser
    {
        IEnumerable<MensajeDto> ParseMensajes(string html);
        MensajeDto ParseMensajesPrivados(string html);
    }
}