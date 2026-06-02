namespace Notificador.Core.Interfaces
{
    public interface ISettingService
    {
        string Url { get; set; }
        string Usuario { get; set; }
        string ClaveEncriptada { get; set; }
        bool ShowMensajesDirector { get; set; }
        bool ShowMensajesJugador { get; set; }
        bool ShowMensajesVIP { get; set; }
        bool ShowMensajesTalleresDirector { get; set; }
        bool ShowMensajesTalleresRedactor { get; set; }
        bool ShowMensajesPrivados { get; set; }
        string IdMensajesDirector { get; set; }
        string IdMensajesJugador { get; set; }
        string IdMensajesVIP { get; set; }
        string IdMensajesTalleresDirector { get; set; }
        string IdMensajesTalleresRedactor { get; set; }
        string IdMensajesPrivados { get; set; }
        string TagMensajesPrivados { get; set; }
        int Espera { get; set; }
        bool Resumido { get; set; }
        bool ShowNotificacionSinMensajes { get; set; }
        string Browser { get; set; }
        bool IniciarMinimizado { get; set; }

        void Save();
    }
}
