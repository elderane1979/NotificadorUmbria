namespace Notificador.Infrastructure.Services
{
    public interface ISettingsProvider
    {
        string Url { get; }
        string Usuario { get; }
        string ClaveEncriptada { get; }
        bool ShowMensajesDirector { get; }
        bool ShowMensajesJugador { get; }
        bool ShowMensajesVIP { get; }
        bool ShowMensajesTalleresDirector { get; }
        bool ShowMensajesTalleresRedactor { get; }
        bool ShowMensajesPrivados { get; }
        string IdMensajesDirector { get; }
        string IdMensajesJugador { get; }
        string IdMensajesVIP { get; }
        string IdMensajesTalleresDirector { get; }
        string IdMensajesTalleresRedactor { get; }
        string IdMensajesPrivados { get; }
        string TagMensajesPrivados { get; }
    }
}