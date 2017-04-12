using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CCP_HTA_2017.Bussines
{
    class RegistroActividad
    {
        private static string archivo = Path.Combine(App.directorioUsuarioAplicacion, Properties.Settings.Default.archivoActividad);        
        public static void escribirActividad(string err)
        {            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archivo, true))
            {
                file.Write(DateTime.Now);
                file.Write(",");
                file.Write(err);
                file.Write("\n");
            }
        }
    }
}
