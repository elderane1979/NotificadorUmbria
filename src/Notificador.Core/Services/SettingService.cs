using Notificador.Core.Interfaces;
using Notificador.Contracts.Interfaces;

namespace Notificador.Core.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingsService _settingsProvider;

        public SettingService(ISettingsService settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }
        public string Url
        {
            get => _settingsProvider.Url;
            set => _settingsProvider.Url = value;
        }

        public string Usuario
        {
            get => _settingsProvider.Usuario;
            set => _settingsProvider.Usuario = value;
        }

        public string ClaveEncriptada
        {
            get => _settingsProvider.ClaveEncriptada;
            set => _settingsProvider.ClaveEncriptada = value;
        }

        public bool ShowMensajesDirector
        {
            get => _settingsProvider.ShowMensajesDirector;
            set => _settingsProvider.ShowMensajesDirector = value;
        }

        public bool ShowMensajesJugador
        {
            get => _settingsProvider.ShowMensajesJugador;
            set => _settingsProvider.ShowMensajesJugador = value;
        }

        public bool ShowMensajesVIP
        {
            get => _settingsProvider.ShowMensajesVIP;
            set => _settingsProvider.ShowMensajesVIP = value;
        }

        public bool ShowMensajesTalleresDirector
        {
            get => _settingsProvider.ShowMensajesTalleresDirector;
            set => _settingsProvider.ShowMensajesTalleresDirector = value;
        }

        public string IdMensajesDirector
        {
            get => _settingsProvider.IdMensajesDirector;
            set => _settingsProvider.IdMensajesDirector = value;
        }

        public string IdMensajesJugador
        {
            get => _settingsProvider.IdMensajesJugador;
            set => _settingsProvider.IdMensajesJugador = value;
        }

        public string IdMensajesVIP
        {
            get => _settingsProvider.IdMensajesVIP;
            set => _settingsProvider.IdMensajesVIP = value;
        }

        public string IdMensajesTalleresDirector
        {
            get => _settingsProvider.IdMensajesTalleresDirector;
            set => _settingsProvider.IdMensajesTalleresDirector = value;
        }

        public string IdMensajesTalleresRedactor
        {
            get => _settingsProvider.IdMensajesTalleresRedactor;
            set => _settingsProvider.IdMensajesTalleresRedactor = value;
        }

        public string IdMensajesPrivados
        {
            get => _settingsProvider.IdMensajesPrivados;
            set => _settingsProvider.IdMensajesPrivados = value;
        }

        public string TagMensajesPrivados
        {
            get => _settingsProvider.TagMensajesPrivados;
            set => _settingsProvider.TagMensajesPrivados = value;
        }

        public bool ShowMensajesTalleresRedactor
        {
            get => _settingsProvider.ShowMensajesTalleresRedactor;
            set => _settingsProvider.ShowMensajesTalleresRedactor = value;
        }

        public bool ShowMensajesPrivados
        {
            get => _settingsProvider.ShowMensajesPrivados;
            set => _settingsProvider.ShowMensajesPrivados = value;
        }

        public int Espera
        {
            get => _settingsProvider.Espera;
            set => _settingsProvider.Espera = value;
        }

        public bool Resumido
        {
            get => _settingsProvider.Resumido;
            set => _settingsProvider.Resumido = value;
        }

        public bool ShowNotificacionSinMensajes
        {
            get => _settingsProvider.ShowNotificacionSinMensajes;
            set => _settingsProvider.ShowNotificacionSinMensajes = value;
        }

        public string Browser
        {
            get => _settingsProvider.Browser;
            set => _settingsProvider.Browser = value;
        }

        public bool IniciarMinimizado
        {
            get => _settingsProvider.IniciarMinimizado;
            set => _settingsProvider.IniciarMinimizado = value;
        }

        public void Save()
        {
            _settingsProvider.Save();
        }
    }
}
