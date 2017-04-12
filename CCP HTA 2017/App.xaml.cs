using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CCP_HTA_2017.Views;
using System.Windows.Data;

namespace CCP_HTA_2017
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    ///     
    public partial class App : Application
    {
        public static string directorioUsuarioAplicacion = Path.Combine(Environment.GetFolderPath(Environment‌​.SpecialFolder.UserProfile),"CCP HTA");
        public static ApplicationViewModel applicationViewModel;
        InicioSesionWindow inicioSesionWindow;
        MainWindow mainWindow;

        void App_Startup(object sender, StartupEventArgs e)
        {
            /* Creación de directorio de archivos generados por la aplicación*/
            System.IO.Directory.CreateDirectory(directorioUsuarioAplicacion);

            /* */
            ObjectDataProvider appDataProvider = this.TryFindResource("applicationViewModel") as ObjectDataProvider;
            applicationViewModel = appDataProvider.ObjectInstance as ApplicationViewModel;

            /* Configuración de lenguaje antes de iniciar la ventana "by default WPF bindings do not respect the current culture." */
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CO");

            /* Inicalización de ventanas xaml*/
            inicioSesionWindow = new InicioSesionWindow();            
            mainWindow = new MainWindow();

            /* Eventos de inicio de sesión y cierre de sesión */
            applicationViewModel.sessionViewModel.SuccessfulLogin += delegate (Object ob, EventArgs ea) 
            {
                inicioSesionWindow.Hide();
                mainWindow.Show();
            };

            applicationViewModel.sessionViewModel.CalledLogout += delegate (Object ob, EventArgs ea)
            {
                mainWindow.Hide();
                inicioSesionWindow.Show();
            };

            /* */
            inicioSesionWindow.Show(); 
        }
    }
}

