using System.Linq;
using Notificador.Infrastructure.Services;
using Xunit;

namespace Notificador.Tests.Notificador.Infrastructure.Test
{
    public class UmbriaHtmlParserTests
    {
        [Fact]
        public void ParseMensajes_ReturnsEmpty_WhenHtmlIsNullOrEmpty()
        {
            var parser = new UmbriaHtmlParser();

            var result1 = parser.ParseMensajes(null);
            var result2 = parser.ParseMensajes(string.Empty);

            Assert.Empty(result1);
            Assert.Empty(result2);
        }

        [Fact]
        public void ParseMensajes_ParsesExpectedValues()
        {
            var html = @"
                <div id='idMensajesDirector'>
                  <ul>
                    <li>
                      <a>Partida A</a>
                      <ul>
                        <li><span>3</span></li>
                        <li><span>2</span></li>
                      </ul>
                    </li>
                    <li>
                      <a>Partida B</a>
                      <ul>
                        <li><span>1</span></li>
                      </ul>
                    </li>
                  </ul>
                </div>
            ";

            var parser = new UmbriaHtmlParser();
            var mensajes = parser.ParseMensajes(html).ToList();

            Assert.Equal(2, mensajes.Count);

            var first = mensajes[0];
            Assert.Equal("Director", first.Tipo);
            Assert.Equal("Partida A", first.Partida);
            Assert.Equal(2, first.Hilos);
            Assert.Equal(5, first.MensajesCount);

            var second = mensajes[1];
            Assert.Equal("Director", second.Tipo);
            Assert.Equal("Partida B", second.Partida);
            Assert.Equal(1, second.Hilos);
            Assert.Equal(1, second.MensajesCount);
        }

        [Fact]
        public void ParseMensajesPrivados_ReturnsNull_WhenHtmlIsNullOrEmpty()
        {
            var parser = new UmbriaHtmlParser();

            var result1 = parser.ParseMensajesPrivados(null);
            var result2 = parser.ParseMensajesPrivados(string.Empty);

            Assert.Null(result1);
            Assert.Null(result2);
        }

        [Fact]
        public void ParseMensajesPrivados_ParsesSpanValue()
        {
            var html = "<div id='idMensajesPrivados'><span>7</span></div>";
            var parser = new UmbriaHtmlParser();

            var result = parser.ParseMensajesPrivados(html);

            Assert.NotNull(result);
            Assert.Equal("Privados", result.Tipo);
            Assert.Equal(7, result.MensajesCount);
            Assert.Equal(0, result.Hilos);
            Assert.Equal(string.Empty, result.Partida);
        }
    }
}
