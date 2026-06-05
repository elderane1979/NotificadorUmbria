using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notificador.App.Process;
using Notificador.Contracts.Interfaces;
using Notificador.Core.Interfaces;
using Notificador.Core.Services;
using Notificador.Infrastructure.Services;
using System;
using System.Windows.Forms;

namespace Notificador.App
{
    class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            // Resuelve el formulario principal desde el contenedor
            var mainForm = ServiceProvider.GetRequiredService<FrmMain>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            //mapper
            services.AddAutoMapper(cfg => { }, typeof(Notificador.Core.DomainMapper.DomainMappingProfile));
            //logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });
            
            // Procesos UI
            services.AddSingleton<IFormMainProcess, FormMainProcess>();

            // Registrar dependencias (servicios)
            services.AddSingleton<ISettingService, SettingService>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<INotificadorService, NotificadorService>();

            //infraestructura
            services.AddSingleton<ISettingsProvider, SettingsProvider>();
            services.AddSingleton<IUmbriaClient, UmbriaClient>();
            services.AddSingleton<IHtmlParser, UmbriaHtmlParser>();
                      
            // Registrar el formulario
            services.AddTransient<FrmMain>();
            services.AddTransient<FrmConf>();
        }
    }
}