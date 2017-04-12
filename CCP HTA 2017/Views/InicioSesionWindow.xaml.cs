using System.Windows;
using System.Windows.Controls;

namespace CCP_HTA_2017.Views
{
    public partial class InicioSesionWindow : Window
    {
        public InicioSesionWindow()
        {
            InitializeComponent();

            loginPassword.Password = "simteg"; // Eliminar (!)
            
            /* Cerrar aplicación si se cierra esta ventana */
            Closed += (s, ea) =>
            {
                Application.Current.Shutdown();
            };
            
        }

        private void contraseña_pbx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /* Actualizar parámetros de 'LoginCommand' */
            object[] param = new object[2];
            param[0] = ((PasswordBox)sender).Password;
            param[1] = ((PasswordBox)sender).Password;
            loginButton.CommandParameter = param;
        }
    }
}
