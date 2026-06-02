namespace Notificador.Infrastructure.Models
{
    public class SettingsFlagsDto
    {
        public bool ShowMensajesDirector { get; set; }
        public bool ShowMensajesJugador { get; set; }
        public bool ShowMensajesVIP { get; set; }
        public bool ShowMensajesTalleresDirector { get; set; }
        public bool ShowMensajesTalleresRedactor { get; set; }
        public bool ShowMensajesPrivados { get; set; }
    }
}