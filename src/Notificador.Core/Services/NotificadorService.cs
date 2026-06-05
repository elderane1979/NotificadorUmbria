using AutoMapper;
using Notificador.Contracts.Helper;
using Notificador.Contracts.Interfaces;
using Notificador.Core.Interfaces;
using Notificador.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notificador.Core.Services
{
    public class NotificadorService : INotificadorService
    {
        private readonly IUmbriaClient _umbriaClient;
        private readonly IHtmlParser _htmlParser;
        private readonly ISettingService _settings;
        private readonly ICryptoService _crypto;
        private readonly IMapper _mapper;

        public NotificadorService(IUmbriaClient umbriaClient, IHtmlParser htmlParser, ISettingService settings, ICryptoService crypto, IMapper mapper)
        {
            _umbriaClient = umbriaClient;
            _htmlParser = htmlParser;
            _settings = settings;
            _crypto = crypto;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Mensaje>> GetNovedadesAsync()
        {
            var clave = string.Empty;
            try
            {
                clave = _crypto.Decrypt(_settings.ClaveEncriptada, Constantes.PASSWORD_KEY);
            }
            catch
            {
                // dejar clave vacía si falla desencriptación
            }

            var html = await _umbriaClient.PostCredentialsAndGetHtmlAsync(_settings.Url, _settings.Usuario, clave);

            var mensajes = new List<Mensaje>();

            var parsedRaw = _htmlParser.ParseMensajes(html);
            var parsed = parsedRaw != null ? _mapper.Map<List<Mensaje>>(parsedRaw) : new List<Mensaje>();


            // Aplicar filtros según settings
            if (_settings.ShowMensajesDirector)
            {
                mensajes.AddRange(parsed.Where(m => m.Tipo == "Director"));
            }
            if (_settings.ShowMensajesJugador)
            {
                mensajes.AddRange(parsed.Where(m => m.Tipo == "Jugador"));
            }
            if (_settings.ShowMensajesVIP)
            {
                mensajes.AddRange(parsed.Where(m => m.Tipo == "VIP"));
            }
            if (_settings.ShowMensajesTalleresDirector)
            {
                mensajes.AddRange(parsed.Where(m => m.Tipo == "Taller (Director)"));
            }
            if (_settings.ShowMensajesTalleresRedactor)
            {
                mensajes.AddRange(parsed.Where(m => m.Tipo == "Taller (Redactor)"));
            }

            if (_settings.ShowMensajesPrivados)
            {
                var privadosRaw = _htmlParser.ParseMensajesPrivados(html);
                var privados = privadosRaw != null ? _mapper.Map<Mensaje>(privadosRaw) : null;
                if (privados != null)
                {
                    mensajes.Add(privados);
                }
            }

            return mensajes;
        }
    }
}