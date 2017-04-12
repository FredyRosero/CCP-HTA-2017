using System;
using System.IO;
using System.Windows.Forms;

namespace CCP_HTA_2017.Bussines 
{
    class RegistroError
    {
        private static string archivo = Path.Combine(App.directorioUsuarioAplicacion, Properties.Settings.Default.archivoErrores);
        public static void escribirError(string error)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archivo, true))
            {
                MessageBox.Show("Se ha presentado el siguiente error:\n " + error);
                if (!String.IsNullOrEmpty(error))
                {
                    string saltoLinea = ((char)0x2028).ToString();
                    string saltoParrafo = ((char)0x2029).ToString();
                    error = error
                        .Replace("\r\n", " - ")
                        .Replace("\n", " - ")
                        .Replace("\r", " - ")
                        .Replace(saltoLinea, " - ")
                        .Replace(saltoParrafo, " - ");
                }
                file.Write( DateTime.Now );
                file.Write(",");
                file.Write(error);
                file.Write("\n");
            }
        }

    }
}
