using HtmlAgilityPack;
using Notificador.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using Notificador.Contracts.Interfaces;

namespace Notificador.Infrastructure.Services
{
    public class UmbriaHtmlParser : IHtmlParser
    {
        public IEnumerable<MensajeDto> ParseMensajes(string html)
        {
            if (string.IsNullOrEmpty(html))
                return Enumerable.Empty<MensajeDto>();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var mensajes = new List<MensajeDto>();

            // Intentar leer nodos por ids conocidos
            var ids = new[] { "idMensajesDirector", "idMensajesJugador", "idMensajesVIP", "idMensajesTalleresDirector", "idMensajesTalleresRedactor" };

            foreach (var id in ids)
            {
                var node = doc.GetElementbyId(id);
                if (node == null)
                    continue;

                var p_node = node.SelectNodes("ul/li");
                if (p_node == null || p_node.Count == 0)
                    continue;

                foreach (var node_partida in p_node)
                {
                    var a_node = node_partida.SelectSingleNode("a");
                    if (a_node == null)
                        continue;

                    var partida = a_node.InnerText.Trim();

                    var m_node = node_partida.SelectNodes("ul/li");
                    int n_hilos = 0;
                    int n_mensajes = 0;

                    if (m_node != null)
                    {
                        n_hilos = m_node.Count;
                        foreach (var node_hilo in m_node)
                        {
                            var span = node_hilo.SelectSingleNode("span");
                            if (span == null)
                                continue;

                            if (int.TryParse(span.InnerText.Trim(), out int count))
                                n_mensajes += count;
                        }
                    }

                    mensajes.Add(new MensajeDto
                    {
                        Hilos = n_hilos,
                        MensajesCount = n_mensajes,
                        Tipo = MapIdToTipo(id),
                        Partida = partida
                    });
                }
            }

            return mensajes;
        }

        public MensajeDto ParseMensajesPrivados(string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Intentar encontrar el id de mensajes privados
            var id = "idMensajesPrivados";
            var node = doc.GetElementbyId(id);
            if (node == null)
                return null;

            // Tag es un selector dentro del nodo, por simplicidad buscar el primer tag <span>
            var span = node.SelectSingleNode("span") ?? node.SelectSingleNode("//span");
            if (span == null)
                return null;

            if (!int.TryParse(span.InnerText.Trim(), out int n_mensajes))
                n_mensajes = 0;

            return new MensajeDto
            {
                Hilos = 0,
                MensajesCount = n_mensajes,
                Tipo = "Privados",
                Partida = string.Empty
            };
        }

        private static string MapIdToTipo(string id)
        {
            switch (id)
            {
                case "idMensajesDirector": return "Director";
                case "idMensajesJugador": return "Jugador";
                case "idMensajesVIP": return "VIP";
                case "idMensajesTalleresDirector": return "Taller (Director)";
                case "idMensajesTalleresRedactor": return "Taller (Redactor)";
                default: return "";
            }
        }
    }
}
