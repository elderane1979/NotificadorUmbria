using Notificador.Infrastructure.Services;
using Notificador.Contracts.Interfaces;
namespace Notificador.Tests.FakeData.Infrastructure
{
    public class FakeSettings : ISettingsService
    {
        public string Url => "http://fake";
        public string Usuario => "user";
        public string ClaveEncriptada => "enc";
        public bool ShowMensajesDirector { get; set; }
        public bool ShowMensajesJugador { get; set; }
        public bool ShowMensajesVIP { get; set; }
        public bool ShowMensajesTalleresDirector { get; set; }
        public bool ShowMensajesTalleresRedactor { get; set; }
        public bool ShowMensajesPrivados { get; set; }
        public string IdMensajesDirector => "id1";
        public string IdMensajesJugador => "id2";
        public string IdMensajesVIP => "id3";
        public string IdMensajesTalleresDirector => "id4";
        public string IdMensajesTalleresRedactor => "id5";
        public string IdMensajesPrivados => "id6";
        public string TagMensajesPrivados => "tag";
    }
}