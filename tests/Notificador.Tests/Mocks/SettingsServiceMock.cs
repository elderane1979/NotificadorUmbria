using Moq;
using Notificador.Core.Interfaces;
using Notificador.Tests.FakeData.Infrastructure;

namespace Notificador.Tests.Mocks
{
    internal class SettingsServiceMock : Mock<ISettingService>
    {
        public SettingsServiceMock()
        {
            SetupDefaults();
        }

        private void SetupDefaults()
        {
            this.SetupAllProperties();

            // Valores por defecto para las propiedades usadas en los tests
            this.SetupProperty(s => s.Usuario, FakeDataUmbriaClient.FAKE_USER_REAL);
            this.SetupProperty(s => s.ClaveEncriptada, FakeDataCrypto.FAKE_ENCRYPTED_PASSWORD);

            this.SetupProperty(s => s.ShowMensajesDirector, true);
            this.SetupProperty(s => s.ShowMensajesJugador, true);
            this.SetupProperty(s => s.ShowMensajesVIP, false);
            this.SetupProperty(s => s.ShowMensajesTalleresDirector, false);
            this.SetupProperty(s => s.ShowMensajesTalleresRedactor, false);
            this.SetupProperty(s => s.ShowMensajesPrivados, true);

            this.SetupProperty(s => s.Url, FakeDataUmbriaClient.FAKE_URL);
            this.SetupProperty(s => s.IdMensajesDirector, "id1");
            this.SetupProperty(s => s.IdMensajesJugador, "id2");
            this.SetupProperty(s => s.IdMensajesVIP, "id3");
            this.SetupProperty(s => s.IdMensajesTalleresDirector, "id4");
            this.SetupProperty(s => s.IdMensajesTalleresRedactor, "id5");
            this.SetupProperty(s => s.IdMensajesPrivados, "id6");
            this.SetupProperty(s => s.TagMensajesPrivados, "tag");
        }
    }
}
