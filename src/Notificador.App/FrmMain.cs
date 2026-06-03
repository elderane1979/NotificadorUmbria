using Microsoft.Extensions.DependencyInjection;
using Notificador.Contracts.Helper;
using Notificador.Contracts.Interfaces;
using Notificador.Core.Interfaces;
using Notificador.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notificador.App
{
    public partial class FrmMain : Form
    {
        float incrementoProgreso = 0;
        float progreso = 0;

        private readonly ISettingsProvider _settingService;
        private readonly INotificadorService _notificadorService;
        private readonly IServiceProvider _serviceProvider;

        public FrmMain(ISettingsProvider settingService, INotificadorService notificadorService, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _settingService = settingService;
            _notificadorService = notificadorService;

            InitializeComponent();

            //version
            this.Text += " v" + Assembly.GetEntryAssembly().GetName().Version.ToString();

            timerProgreso.Interval = 1000;
            timerProgreso.Tick += TimerProgreso_Tick;
            timerProgreso.Start();

            CalcularIntervalo();
            LblNext.Text = "Próxima comprobación: " + (DateTime.Now.AddMinutes(_settingService.Espera));

            progreso = ProgressBar.Minimum;
            ProgressBar.Value = ProgressBar.Minimum;

            notifyIcon1.BalloonTipTitle = "Notificador Mensajes Umbría";
            notifyIcon1.BalloonTipText = Constantes.ESPERANDO;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            ComprobarAsync();
        }

        private void TimerProgreso_Tick(object sender, EventArgs e)
        {
            progreso += incrementoProgreso;
            if (progreso >= ProgressBar.Maximum)
            {
                ComprobarAsync();

                progreso = ProgressBar.Minimum;
                ProgressBar.Value = ProgressBar.Minimum;
            }
            ProgressBar.Value = (int)(progreso);
        }
        private void CalcularIntervalo()
        {/*
            Properties.Settings.Default.Espera * 60 --> 100
                1 --> ¿¿
                */
            incrementoProgreso = ((float)(ProgressBar.Maximum - ProgressBar.Minimum) / (_settingService.Espera * 60));
        }
        private async Task ComprobarAsync()
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    throw new NetworkInformationException();
                }

                var result = await _notificadorService.GetNovedadesAsync();

                var resultResumido = CrearMensaje(result, true);
                var resultMsg = CrearMensaje(result, _settingService.Resumido);

                tbResumen.Text = resultMsg;
                notifyIcon1.BalloonTipText = resultMsg;
                string notifyText = "Novedades Umbria:" + Environment.NewLine + resultResumido;
                if (notifyText.Length > 64)
                    notifyText = notifyText.Substring(0, 61) + "...";
                notifyIcon1.Text = notifyText;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                lblMsgToolStripMenuItem.Text = resultResumido;

                if (resultMsg != Constantes.NO_MENSAJES
                    || _settingService.ShowNotificacionSinMensajes)
                    notifyIcon1.ShowBalloonTip(2000);

                LblLast.Text = "Última comprobación: " + DateTime.Now;
                LblNext.Text = "Siguiente comprobación: " + DateTime.Now.AddMinutes(_settingService.Espera);

            }
            catch (Exception ex)
            {
                tbResumen.Text = "Se ha encontrado un error inesperado: " + ex.Message;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon1.BalloonTipText = tbResumen.Text;
            }
            finally
            {
                LblNext.Text = "Próxima comprobación: " + (DateTime.Now.AddMinutes(_settingService.Espera));
                progreso = ProgressBar.Minimum;
                ProgressBar.Value = ProgressBar.Minimum;
            }
        }
        private string CrearMensaje(IEnumerable<Mensaje> mensajes, bool resumido)
        {
            string Mensaje = String.Empty;
            int total_mensajes = mensajes
                .Where(msg => msg.Tipo != Constantes.MENSAJES_PRIVADOS)
                .Sum(msg => msg.MensajesCount);
            int total_mensajes_privados = mensajes
                .Where(msg => msg.Tipo == Constantes.MENSAJES_PRIVADOS)
                .Sum(msg => msg.MensajesCount);
            int total_hilos = mensajes.Sum(msg => msg.Hilos);

            if (total_mensajes + total_mensajes_privados > 0)
            {
                if (resumido)
                {
                    Mensaje = String.Empty;
                    if (total_mensajes > 0)
                    {
                        Mensaje += String.Format("{0} mensaje{2} en {1} hilo{3}",
                            total_mensajes,
                            total_hilos,
                            total_mensajes > 1 ? "s" : "",
                            total_hilos > 1 ? "s" : "");
                    }
                    if (total_mensajes_privados > 0)
                    {
                        if (!String.IsNullOrEmpty(Mensaje))
                            Mensaje += Environment.NewLine;
                        Mensaje += String.Format("{0} mensaje{1} privado{1}",
                            total_mensajes_privados,
                            total_mensajes_privados > 1 ? "s" : "");
                    }
                }
                else
                {
                    foreach (Mensaje mensaje in mensajes)
                    {
                        if (mensaje.MensajesCount > 0)
                        {
                            if (!String.IsNullOrEmpty(Mensaje))
                                Mensaje += Environment.NewLine;
                            Mensaje += String.Format("{0} mensaje{2} nuevo{2} como {1} ",
                               mensaje.MensajesCount,
                               mensaje.Tipo,
                                total_mensajes > 1 ? "s" : "");
                            if (mensaje.Tipo != Constantes.MENSAJES_PRIVADOS)
                                Mensaje += String.Format("en {0} hilo{1}",
                                        mensaje.Hilos,
                                        total_hilos > 1 ? "s" : "");
                            if (!String.IsNullOrEmpty(mensaje.Partida))
                                Mensaje += String.Format(" en la partida {0}", mensaje.Partida);
                        }
                    }
                }
            }
            else
            {
                Mensaje = Constantes.NO_MENSAJES;
            }
            return Mensaje;
        }

        private void BtnComprobar_Click(object sender, EventArgs e)
        {
            ComprobarAsync();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs &&
                (e as MouseEventArgs).Button == MouseButtons.Left)
                IrANovedades();
        }
        private void btnNovedades_Click(object sender, EventArgs e)
        {
            IrANovedades();

        }
        private void irANovedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrANovedades();
        }

        private void IrANovedades()
        {
            if (String.IsNullOrEmpty(_settingService.Browser) ||
                _settingService.Browser == Constantes.DEFAULT_BROWSER)
                System.Diagnostics.Process.Start(_settingService.Url);
            else
                System.Diagnostics.Process.Start(_settingService.Browser, _settingService.Url);

        }

        private void BtnConfiguración_Click(object sender, EventArgs e)
        {
            Configurar();
        }

        private void Configurar()
        {
            FrmConf frm = _serviceProvider.GetRequiredService<FrmConf>();

            if (frm.ShowDialog() == DialogResult.OK)
            {

                CalcularIntervalo();
                ProgressBar.Value = ProgressBar.Minimum;
                LblNext.Text = "Próxima comprobación: " + (DateTime.Now.AddMinutes(_settingService.Espera));
            }
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configurar();
        }

        private void comprogarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComprobarAsync();
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_settingService.IniciarMinimizado)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Form1_Resize(this, null);
            }
        }
    }
}
