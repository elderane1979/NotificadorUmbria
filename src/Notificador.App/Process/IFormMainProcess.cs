using Notificador.App.Models;
using System.Threading.Tasks;

namespace Notificador.App.Process
{
    public interface IFormMainProcess
    {
        Task<MensajeUI> ComprobarAsync();
        void IrANovedades();
        float CalcularIntervalo(int MinValue, int MaxValue, int TiempoTotal);
    }
}