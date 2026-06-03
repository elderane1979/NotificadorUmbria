using System.Collections.Specialized;
using System.Reflection;
using Notificador.Infrastructure.Services;
using Xunit;

namespace Notificador.Tests.Notificador.Infrastructure.Test
{
    public class SettingsProviderTests
    {
        [Fact]
        public void Espera_ReturnsDefault_WhenValueIsNotInteger()
        {
            var sp = new SettingsProvider();
            var coll = new NameValueCollection { { "espera", "notanint" } };

            typeof(SettingsProvider)
                .GetField("_appSettings", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(sp, coll);

            Assert.Equal(5, sp.Espera);
        }

        [Fact]
        public void GettersAndSetters_WriteToInternalCollection()
        {
            var sp = new SettingsProvider();
            var coll = new NameValueCollection();

            typeof(SettingsProvider)
                .GetField("_appSettings", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(sp, coll);

            sp.Url = "http://test";
            sp.Usuario = "user1";
            sp.ClaveEncriptada = "enc";
            sp.ShowMensajesDirector = true;
            sp.ShowMensajesJugador = false;
            sp.IdMensajesDirector = "idD";
            sp.TagMensajesPrivados = "tag";
            sp.Espera = 10;

            Assert.Equal("http://test", coll["url"]);
            Assert.Equal("user1", coll["usuario"]);
            Assert.Equal("enc", coll["clave"]);
            Assert.Equal("True", coll["showMensajesDirector"]);
            Assert.Equal("False", coll["showMensajesJugador"]);
            Assert.Equal("idD", coll["idMensajesDirector"]);
            Assert.Equal("tag", coll["tagMensajesPrivados"]);
            Assert.Equal("10", coll["espera"]);

            // And getters forward to the collection
            Assert.Equal("http://test", sp.Url);
            Assert.Equal("user1", sp.Usuario);
            Assert.Equal("enc", sp.ClaveEncriptada);
            Assert.True(sp.ShowMensajesDirector);
            Assert.False(sp.ShowMensajesJugador);
            Assert.Equal("idD", sp.IdMensajesDirector);
            Assert.Equal("tag", sp.TagMensajesPrivados);
            Assert.Equal(10, sp.Espera);
        }
    }
}
