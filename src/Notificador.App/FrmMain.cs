using Microsoft.Extensions.DependencyInjection;
using Notificador.App.Process;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly IFormMainProcess _formMainProcess;

        public FrmMain(ISettingsProvider settingService, 
                       IServiceProvider serviceProvider,
                       IFormMainProcess formMainProcess)
        {
            _serviceProvider = serviceProvider;
            _settingService = settingService;
            _formMainProcess = formMainProcess;

            InitializeComponent();


            //version
            this.Text += " v" + Assembly.GetEntryAssembly().GetName().Version.ToString();

            InitTimer();

            LblNext.Text = "Próxima comprobación: " + (DateTime.Now.AddMinutes(_settingService.Espera));

            InitProgressBar();

            InitNotifyIcon();

            ComprobarAsync();
        }

        private void InitTimer()
        {
            timerProgreso.Interval = 1000;
            timerProgreso.Tick += TimerProgreso_Tick;
            timerProgreso.Start();
        }

        private void InitProgressBar()
        {
            _formMainProcess.CalcularIntervalo(ProgressBar.Minimum, ProgressBar.Maximum, _settingService.Espera);
            progreso = ProgressBar.Minimum;
            ProgressBar.Value = ProgressBar.Minimum;
        }

        private void InitNotifyIcon()
        {
            // Asegurar que el NotifyIcon tiene un icono para mostrarse en la bandeja
            if (notifyIcon1.Icon == null)
            {
                notifyIcon1.Icon = System.Drawing.SystemIcons.Application;
            }
            notifyIcon1.BalloonTipTitle = "Notificador Mensajes Umbría";
            notifyIcon1.BalloonTipText = Constantes.ESPERANDO;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
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

        private async Task ComprobarAsync()
        {
            try
            {
                var mensajeUI = await _formMainProcess.ComprobarAsync();

                tbResumen.Text = mensajeUI.Mensaje;
                notifyIcon1.BalloonTipText = mensajeUI.Mensaje; 
                string notifyText = "Novedades Umbria:" + Environment.NewLine + mensajeUI.MensajeResumido ;
                if (notifyText.Length > 64)
                {
                    notifyText = notifyText.Substring(0, 61) + "...";
                }
                notifyIcon1.Text = notifyText;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                lblMsgToolStripMenuItem.Text = mensajeUI.MensajeResumido ;

                if (mensajeUI.Mensaje != Constantes.NO_MENSAJES
                    || _settingService.ShowNotificacionSinMensajes)
                {
                    notifyIcon1.ShowBalloonTip(2000);
                }

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
               _formMainProcess.IrANovedades();
        }
        private void btnNovedades_Click(object sender, EventArgs e)
        {
            _formMainProcess.IrANovedades();
        }
        private void irANovedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formMainProcess.IrANovedades();
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

                _formMainProcess.CalcularIntervalo(ProgressBar.Minimum, ProgressBar.Maximum, _settingService.Espera);
                ProgressBar.Value = ProgressBar.Minimum;
                LblNext.Text = "Próxima comprobación: " + (DateTime.Now.AddMinutes(_settingService.Espera));
            }
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configurar();
        }

        private void comprobarToolStripMenuItem_Click(object sender, EventArgs e)
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
