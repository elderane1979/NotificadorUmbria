using System.Threading.Tasks;

namespace Notificador.Contracts.Interfaces
{
    public interface IUmbriaClient
    {
        Task<string> PostCredentialsAndGetHtmlAsync(string url, string usuario, string clave);
    }
}