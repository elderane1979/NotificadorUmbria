using System.Threading.Tasks;

namespace Notificador.Infrastructure.Services
{
    public interface IUmbriaClient
    {
        Task<string> PostCredentialsAndGetHtmlAsync(string url, string usuario, string clave);
    }
}