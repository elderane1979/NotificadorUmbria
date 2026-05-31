using System.Collections.Generic;
using System.Threading.Tasks;
using Notificador.Core.Models;

namespace Notificador.Core.Interfaces
{
    public interface INotificadorService
    {
        Task<IEnumerable<Mensaje>> GetNovedadesAsync();
    }
}