using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCP_HTA_2017.ViewModels;
using System.Diagnostics;
using System.ComponentModel;

namespace CCP_HTA_2017Tests.ViewModels
{
    [TestClass]
    public class AppVm_Inicialización_y_carrga_de_datos
    {

        //Arragment        
        string contraseñaCorrecta = "simteg";
        string contraseñaIncorrecta = "contraseñaIncorrecta";

        [TestMethod]
        public void AppVm_inicialización()
        {
            ApplicationViewModel AppVm = new ApplicationViewModel();
        }

        [TestMethod]
        public void AppVm_Lectura_de_tablas_desautorizada()
        {
            //login incorrecto
            ApplicationViewModel AppVm = new ApplicationViewModel();        
            AppVm.sessionViewModel.SuccessfulLogin += delegate (Object s, EventArgs e) { Trace.WriteLine("SuccessfulLogin called."); };
            AppVm.sessionViewModel.CalledLogout += delegate (Object s, EventArgs e) { Trace.WriteLine("CalledLogout called."); };
            AppVm.sessionViewModel.actionResponse.PropertyChanged += delegate (object s, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "responseText")
                    Trace.WriteLine(">> PropertyChanged called. sessionVm.actionResponse.responseText = " + AppVm.sessionViewModel.actionResponse.responseText);
            };
            Trace.WriteLine("AppVm.sessionViewModel.Logout();");
            AppVm.sessionViewModel.Logout();
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine("AppVm.sessionViewModel.login(contraseñaIncorrecta);");
            AppVm.sessionViewModel.Login(contraseñaIncorrecta);
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsTrue(AppVm.pacienteTableViewModel.tableViewModel.Count == 0);

            //Sesión cerrada
            Trace.WriteLine("\n"); 
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine(" AppVm.sessionViewModel.Logout();");
            AppVm.sessionViewModel.Logout();
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsTrue(AppVm.pacienteTableViewModel.tableViewModel.Count == 0);
        }

        [TestMethod]
        public void AppVm_Lectura_de_tablas_autorizada()
        {
            ApplicationViewModel AppVm = new ApplicationViewModel();
            AppVm.sessionViewModel.SuccessfulLogin += delegate (Object s, EventArgs e) { Trace.WriteLine("SuccessfulLogin called."); };
            AppVm.sessionViewModel.CalledLogout += delegate (Object s, EventArgs e) { Trace.WriteLine("CalledLogout called."); };
            AppVm.sessionViewModel.actionResponse.PropertyChanged += delegate (object s, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "responseText")
                    Trace.WriteLine(">> PropertyChanged called. sessionVm.actionResponse.responseText = " + AppVm.sessionViewModel.actionResponse.responseText);
            };

            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine("AppVm.sessionViewModel.login(contraseñaCorrecta);");
            AppVm.sessionViewModel.Login(contraseñaCorrecta);
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsFalse(AppVm.pacienteTableViewModel.tableViewModel.Count == 0);

            //Sesión cerrada
            Trace.WriteLine("\n");
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine(" AppVm.sessionViewModel.Logout();");
            AppVm.sessionViewModel.Logout();
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsTrue(AppVm.pacienteTableViewModel.tableViewModel.Count == 0);

            //Iniciar sesipon
            Trace.WriteLine("\n");
            Trace.WriteLine("AppVm.sessionViewModel.login(contraseñaCorrecta);");
            AppVm.sessionViewModel.Login(contraseñaCorrecta);
            Trace.WriteLine("\t AppVm.pacienteTableViewModel.tableViewModel.Count=" + AppVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsFalse(AppVm.pacienteTableViewModel.tableViewModel.Count == 0);
        }

    }
}
