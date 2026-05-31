using System.Collections.Generic;
using Notificador.Core.Models;

namespace Notificador.Infrastructure.Services
{
    public interface IHtmlParser
    {
        IEnumerable<Mensaje> ParseMensajes(string html);
        Mensaje ParseMensajesPrivados(string html);
    }
}