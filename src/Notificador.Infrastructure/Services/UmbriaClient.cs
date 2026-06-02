using System;
using System.Net.Http;
using System.Threading.Tasks;
using Notificador.Contracts.Interfaces;
using Notificador.Contracts.Helper;

namespace Notificador.Infrastructure.Services
{
    public class UmbriaClient : IUmbriaClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        public UmbriaClient()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> PostCredentialsAndGetHtmlAsync(string url, string usuario, string clave)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    // Field names use constants defined in the original codebase if available
                    var accesoField = Constantes.ACCESO;
                    var claveField = Constantes.CLAVE;

                    if (string.IsNullOrEmpty(accesoField))
                    {
                        accesoField = "usuario";
                    }
                    if (string.IsNullOrEmpty(claveField))
                    {
                        claveField = "clave";
                    }

                    formData.Add(new StringContent(usuario ?? string.Empty), accesoField);
                    formData.Add(new StringContent(clave ?? string.Empty), claveField);

                    var response = await _httpClient.PostAsync(url, formData).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        return string.Empty;
                    }

                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return data ?? string.Empty;
                }
            }
            catch
            {
                // En caso de error de red o parsing, devolver cadena vacía para que el servicio lo maneje
                return string.Empty;
            }
        }
        ~UmbriaClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(Boolean disposing)
        {

            if (_disposed) return;
            
            if (disposing)
            {
                _httpClient.Dispose();
            }
            _disposed = true;

        }
    }
}
