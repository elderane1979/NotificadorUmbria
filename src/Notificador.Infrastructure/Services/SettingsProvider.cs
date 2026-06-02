using System.Configuration;
using Notificador.Contracts.Interfaces;

namespace Notificador.Infrastructure.Services
{
    /// <summary>
    /// Proveedor de configuración que lee AppSettings del archivo de configuración.
    /// Se usa como fallback cuando no es posible acceder a Properties.Settings desde otro ensamblado.
    /// </summary>
    public class SettingsProvider : ISettingsService
    {
        private readonly System.Collections.Specialized.NameValueCollection _appSettings;

        public SettingsProvider()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        private string GetString(string key)
        {
            var v = _appSettings[key];
            return v ?? string.Empty;
        }

        private bool GetBool(string key)
        {
            var v = _appSettings[key];
            bool.TryParse(v, out var result);
            return result;
        }

        private int GetInt(string key)
        {
            var v = _appSettings[key];
            if (!int.TryParse(v, out var result))
            {
                throw new System.FormatException($"La clave de configuración '{key}' no contiene un entero válido: '{v}'");
            }
            return result;
        }

        public string Url
        {
            get
            {
               return GetString("url");
            }
            set
            {
                _appSettings["url"] = value;
            }
        }
        public string Usuario
        {
            get { return GetString("usuario"); }
            set { _appSettings["usuario"] = value; }
        }

        public string ClaveEncriptada
        {
            get { return GetString("clave"); }
            set { _appSettings["clave"] = value; }
        }

        public bool ShowMensajesDirector
        {
            get { return GetBool("showMensajesDirector"); }
            set { _appSettings["showMensajesDirector"] = value.ToString(); }
        }

        public bool ShowMensajesJugador
        {
            get { return GetBool("showMensajesJugador"); }
            set { _appSettings["showMensajesJugador"] = value.ToString(); }
        }

        public bool ShowMensajesVIP
        {
            get { return GetBool("showMensajesVIP"); }
            set { _appSettings["showMensajesVIP"] = value.ToString(); }
        }

        public bool ShowMensajesTalleresDirector
        {
            get { return GetBool("showMensajesTalleresDirector"); }
            set { _appSettings["showMensajesTalleresDirector"] = value.ToString(); }
        }

        public bool ShowMensajesTalleresRedactor
        {
            get { return GetBool("showMensajesTalleresRedactor"); }
            set { _appSettings["showMensajesTalleresRedactor"] = value.ToString(); }
        }

        public bool ShowMensajesPrivados
        {
            get { return GetBool("showMensajesPrivados"); }
            set { _appSettings["showMensajesPrivados"] = value.ToString(); }
        }

        public string IdMensajesDirector
        {
            get { return GetString("idMensajesDirector"); }
            set { _appSettings["idMensajesDirector"] = value; }
        }

        public string IdMensajesJugador
        {
            get { return GetString("idMensajesJugador"); }
            set { _appSettings["idMensajesJugador"] = value; }
        }

        public string IdMensajesVIP
        {
            get { return GetString("idMensajesVIP"); }
            set { _appSettings["idMensajesVIP"] = value; }
        }

        public string IdMensajesTalleresDirector
        {
            get { return GetString("idMensajesTalleresDirector"); }
            set { _appSettings["idMensajesTalleresDirector"] = value; }
        }

        public string IdMensajesTalleresRedactor
        {
            get { return GetString("idMensajesTalleresRedactor"); }
            set { _appSettings["idMensajesTalleresRedactor"] = value; }
        }

        public string IdMensajesPrivados
        {
            get { return GetString("idMensajesPrivados"); }
            set { _appSettings["idMensajesPrivados"] = value; }
        }

        public string TagMensajesPrivados
        {
            get { return GetString("tagMensajesPrivados"); }
            set { _appSettings["tagMensajesPrivados"] = value; }
        }

        public int Espera
        {
            get
            {
                try
                {
                    return GetInt("espera");
                }
                catch (System.Exception)
                {
                    // Si el valor no es un entero válido, devolvemos el valor por defecto 5
                    return 5;
                }
            }
            set { _appSettings["espera"] = value.ToString(); }
        }

        public bool Resumido
        {
            get { return GetBool("resumido"); }
            set { _appSettings["resumido"] = value.ToString(); }
        }

        public bool ShowNotificacionSinMensajes
        {
            get { return GetBool("showNotificacionSinMensajes"); }
            set { _appSettings["showNotificacionSinMensajes"] = value.ToString(); }
        }

        public string Browser
        {
            get { return GetString("browser"); }
            set { _appSettings["browser"] = value; }
        }

        public bool IniciarMinimizado
        {
            get { return GetBool("iniciarMinimizado"); }
            set { _appSettings["iniciarMinimizado"] = value.ToString(); }
        }

        public void Save()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = config.AppSettings.Settings;

                void Set(string key, string value)
                {
                    if (settings[key] == null)
                        settings.Add(key, value ?? string.Empty);
                    else
                        settings[key].Value = value ?? string.Empty;
                }

                Set("url", Url);
                Set("usuario", Usuario);
                Set("clave", ClaveEncriptada);
                Set("showMensajesDirector", ShowMensajesDirector.ToString());
                Set("showMensajesJugador", ShowMensajesJugador.ToString());
                Set("showMensajesVIP", ShowMensajesVIP.ToString());
                Set("showMensajesTalleresDirector", ShowMensajesTalleresDirector.ToString());
                Set("showMensajesTalleresRedactor", ShowMensajesTalleresRedactor.ToString());
                Set("showMensajesPrivados", ShowMensajesPrivados.ToString());
                Set("idMensajesDirector", IdMensajesDirector);
                Set("idMensajesJugador", IdMensajesJugador);
                Set("idMensajesVIP", IdMensajesVIP);
                Set("idMensajesTalleresDirector", IdMensajesTalleresDirector);
                Set("idMensajesTalleresRedactor", IdMensajesTalleresRedactor);
                Set("idMensajesPrivados", IdMensajesPrivados);
                Set("tagMensajesPrivados", TagMensajesPrivados);
                Set("espera", Espera.ToString());
                Set("resumido", Resumido.ToString());
                Set("showNotificacionSinMensajes", ShowNotificacionSinMensajes.ToString());
                Set("browser", Browser);
                Set("iniciarMinimizado", IniciarMinimizado.ToString());

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"SettingsProvider.Save error: {ex}");
                throw;
            }
        }
    }
}