using Microsoft.Win32;
using Notificador.Contracts.Helper;
using Notificador.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Notificador.App
{
    public partial class FrmConf : Form
    {
        private readonly ISettingService _settingService;
        private readonly ICryptoService _cryptoService;

        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        const string AppNotificadorUmbria = "NotificadorUmbria";

        public FrmConf(ISettingService settingService, ICryptoService cryptoService)
        {
            InitializeComponent();
            _settingService = settingService;
            _cryptoService = cryptoService;

            //Cargar Navegadores Instalados
            cmbBrowser.DataSource = LoadInstalledBrowsers();
            cmbBrowser.DisplayMember = "Item1";
            cmbBrowser.ValueMember = "Item2";
        }
        private List<Tuple<string, string>> LoadInstalledBrowsers()
        {
            List<Tuple<string, string>> lst = new List<Tuple<string, string>>();
            lst.Add(new Tuple<string, string>(Constantes.DEFAULT_BROWSER, ""));

            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                RegistryKey webClientsRootKey = hklm.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                if (webClientsRootKey != null)
                {
                    foreach (var subKeyName in webClientsRootKey.GetSubKeyNames())
                    {
                        string commandLineUri = GetShellOpenCommand(webClientsRootKey, subKeyName);
                        if (string.IsNullOrEmpty(commandLineUri))
                            continue;
                        commandLineUri = commandLineUri.Trim("\"".ToCharArray());
                        Tuple<string, string> newBrowser = new Tuple<string, string>((string)webClientsRootKey.OpenSubKey(subKeyName).GetValue(null), commandLineUri);

                        lst.Add(newBrowser);
                    }
                }
            }
            return lst;
        }

        private static string GetShellOpenCommand(RegistryKey webClientsRootKey, string subKeyName)
        {
            const string RegEditShellKey = "shell";
            const string RegEditOpenKey = "open";
            const string RegEditCommandKey = "command";
            string commandLineUri = "";

            var l1 = GetRegistryKey(webClientsRootKey, subKeyName);
            var l2 = GetRegistryKey(l1, RegEditShellKey);
            var l3 = GetRegistryKey(l2, RegEditOpenKey);
            var l4 = GetRegistryKey(l3, RegEditCommandKey);

            if(l4 != null)
            {
                commandLineUri = (string)l4.GetValue(null);
            }

            return commandLineUri;
        }

        private static RegistryKey GetRegistryKey(RegistryKey rootKey, string subKeyName)
        {
            if(rootKey == null ||
               string.IsNullOrEmpty(subKeyName))
            {
                return null;
            }

            return rootKey.OpenSubKey(subKeyName);
        }

        private void FrmConf_Load(object sender, EventArgs e)
        {
            tbUsuario.Text = _settingService.Usuario;
            try
            {
                tbPassword.Text = _cryptoService.Decrypt(_settingService.ClaveEncriptada, Constantes.PASSWORD_KEY);
            }
            catch
            {
                tbPassword.Text = "";
            }
            tbIntervalo.Value =  _settingService.Espera;
            chkDirector.Checked =  _settingService.ShowMensajesDirector;
            chkJugador.Checked =  _settingService.ShowMensajesJugador;
            chkPrivados.Checked =  _settingService.ShowMensajesPrivados;
            chkResumida.Checked =  _settingService.Resumido;
            chkNotificacionSinMensajes.Checked =  _settingService.ShowNotificacionSinMensajes;
            chkMinimizado.Checked =  _settingService.IniciarMinimizado;
            chkVips.Checked =  _settingService.ShowMensajesVIP;
            chkTallerRedactor.Checked =  _settingService.ShowMensajesTalleresRedactor;
            chkTallerDirector.Checked =  _settingService.ShowMensajesTalleresDirector;
            if ( _settingService.Browser == Constantes.DEFAULT_BROWSER)
                cmbBrowser.SelectedValue = "";
            else
                cmbBrowser.SelectedValue =  _settingService.Browser;

            chkStartUp.Checked = rkApp.GetValue(AppNotificadorUmbria) != null;

        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
             _settingService.Usuario = tbUsuario.Text;
             _settingService.ClaveEncriptada = _cryptoService.Encrypt(tbPassword.Text, Constantes.PASSWORD_KEY);
             _settingService.Espera = (int)tbIntervalo.Value;
             _settingService.ShowMensajesDirector = chkDirector.Checked;
             _settingService.ShowMensajesJugador = chkJugador.Checked;
             _settingService.ShowMensajesPrivados = chkJugador.Checked;
             _settingService.Resumido = chkResumida.Checked;
             _settingService.ShowNotificacionSinMensajes = chkNotificacionSinMensajes.Checked;
             _settingService.IniciarMinimizado = chkMinimizado.Checked;
             _settingService.ShowMensajesVIP = chkVips.Checked;
             _settingService.ShowMensajesTalleresRedactor = chkTallerRedactor.Checked;
             _settingService.ShowMensajesTalleresDirector = chkTallerDirector.Checked;
            if (cmbBrowser.SelectedValue.ToString() == Constantes.DEFAULT_BROWSER)
                 _settingService.Browser = Constantes.DEFAULT_BROWSER;
            else
                 _settingService.Browser = cmbBrowser.SelectedValue.ToString();

             _settingService.Save();

            if (chkStartUp.Checked)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue(AppNotificadorUmbria, Application.ExecutablePath);
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue(AppNotificadorUmbria, false);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
