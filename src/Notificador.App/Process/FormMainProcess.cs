using Microsoft.Extensions.Primitives;
using Notificador.App.Models;
using Notificador.Contracts.Helper;
using Notificador.Core.Interfaces;
using Notificador.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Notificador.App.Process
{
    public class FormMainProcess : IFormMainProcess
    {
        private readonly ISettingService _settingService;
        private readonly INotificadorService _notificadorService;
        public FormMainProcess(ISettingService settingService,
                       INotificadorService notificadorService)
        {
            _settingService = settingService;
            _notificadorService = notificadorService;
        }

        public async Task<MensajeUI> ComprobarAsync()
        {
            var mensajeUI = new MensajeUI();
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    throw new NetworkInformationException();
                }

                var result = await _notificadorService.GetNovedadesAsync();

                mensajeUI.MensajeResumido = CrearMensaje(result, true);
                mensajeUI.Mensaje = CrearMensaje(result, _settingService.Resumido);

                return mensajeUI;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al comprobar novedades", ex);
            }
        }

        private static string CrearMensaje(IEnumerable<Mensaje> mensajes, bool resumido)
        {
            string Mensaje;

            if (mensajes.Any())
            {
                if (resumido)
                {
                    Mensaje = CrearMensajeResumido(mensajes);
                }
                else
                {
                    Mensaje = CrearMensajeCompleto(mensajes);
                }
            }
            else
            {
                Mensaje = Constantes.NO_MENSAJES;
            }
            return Mensaje;
        }

        private static string CrearMensajeCompleto(IEnumerable<Mensaje> mensajes)
        {
            StringBuilder mensajeBuild = new StringBuilder();
            foreach (Mensaje mensaje in mensajes)
            {
                CrearLineaDeMensaje( mensajeBuild, mensaje);
            }

            return mensajeBuild.ToString();
        }

        private static void CrearLineaDeMensaje(StringBuilder mensajeBuild, Mensaje mensaje)
        {
            if (mensaje.MensajesCount > 0)
            {
                if (!String.IsNullOrEmpty(mensajeBuild.ToString()))
                    mensajeBuild.Append(Environment.NewLine);
                mensajeBuild.Append(String.Format("{0} mensaje{2} nuevo{2} como {1} ",
                   mensaje.MensajesCount,
                   mensaje.Tipo,
                   mensaje.MensajesCount > 1 ? "s" : ""));
                if (mensaje.Tipo != Constantes.MENSAJES_PRIVADOS)
                    mensajeBuild.Append(String.Format("en {0} hilo{1}",
                            mensaje.Hilos,
                            mensaje.Hilos > 1 ? "s" : ""));
                if (!String.IsNullOrEmpty(mensaje.Partida))
                    mensajeBuild.Append(String.Format(" en la partida {0}", mensaje.Partida));
            }
        }

        private static string CrearMensajeResumido(IEnumerable<Mensaje> mensajes)
        {
            StringBuilder Mensaje = new StringBuilder();
            int total_mensajes = mensajes
                .Where(msg => msg.Tipo != Constantes.MENSAJES_PRIVADOS)
                .Sum(msg => msg.MensajesCount);
            int total_mensajes_privados = mensajes
                .Where(msg => msg.Tipo == Constantes.MENSAJES_PRIVADOS)
                .Sum(msg => msg.MensajesCount);
            int total_hilos = mensajes.Sum(msg => msg.Hilos);
            if (total_mensajes > 0)
            {
                Mensaje.Append( String.Format("{0} mensaje{2} en {1} hilo{3}",
                    total_mensajes,
                    total_hilos,
                    total_mensajes > 1 ? "s" : "",
                    total_hilos > 1 ? "s" : ""));
            }
            if (total_mensajes_privados > 0)
            {
                if (Mensaje.Length>0)
                    Mensaje.Append(Environment.NewLine);
                Mensaje.Append( String.Format("{0} mensaje{1} privado{1}",
                    total_mensajes_privados,
                    total_mensajes_privados > 1 ? "s" : ""));
            }

            return Mensaje.ToString();
        }

        public void IrANovedades()
        {
            if (String.IsNullOrEmpty(_settingService.Browser) ||
                _settingService.Browser == Constantes.DEFAULT_BROWSER)
                System.Diagnostics.Process.Start(_settingService.Url);
            else
                System.Diagnostics.Process.Start(_settingService.Browser, _settingService.Url);

        }

        public float CalcularIntervalo(int MinValue, int MaxValue, int TiempoTotal)
        {/*
            Properties.Settings.Default.Espera * 60 --> 100
                1 --> ¿¿
                */
            return ((float)(MaxValue - MinValue) / (TiempoTotal * 60));
        }
    }
}
