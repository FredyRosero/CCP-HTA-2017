using System;
using System.Windows;

namespace CCP_HTA_2017.Views
{
    /// <summary>
    /// Interaction logic for cambiarContraseña.xaml
    /// </summary>
    public partial class CambioContraseñaWindow : Window
    {
        public CambioContraseñaWindow()
        {
            InitializeComponent();
        }
        public bool ValidaciónContraseña()
        {
            if (String.IsNullOrWhiteSpace(contraseña_pwb.Password) || String.IsNullOrWhiteSpace(repeticiónContraseña_pwb.Password))
            {
                MessageBox.Show("La campos no pueden estar vacíos");
                return false;
            }
            if (contraseña_pwb.Password != repeticiónContraseña_pwb.Password)
            {
                MessageBox.Show("Las contraseñas no coínciden");
                return false;
            }
            return true;
        }
    }

}
