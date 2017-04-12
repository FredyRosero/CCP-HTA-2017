using System;
using System.ComponentModel;
using System.Diagnostics;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CCP_HTA_2017Tests.ViewModels
{
    [TestClass]
    public class SessionVm_Test
    {
        //Arragment 
        string nombreUsuarioPorDefecto = "default";
        string contraseñaCorrectaVieja = "simteg";
        string contraseñaIncorrecta = "contraseñaIncorrecta";
        string contraseñaNueva = "contraseñaNueva";
        ApplicationDataAccess appDataAccess;
        SessionViewModel sessionVm;
        public SessionVm_Test()
        {
            appDataAccess = new ApplicationDataAccess();
            sessionVm = new SessionViewModel(appDataAccess);
            sessionVm.SuccessfulLogin += delegate (Object s, EventArgs e) { Trace.WriteLine(">> SuccessfulLogin called. IsUserLogged:" + sessionVm.IsUserLogged); };
            sessionVm.CalledLogout += delegate (Object s, EventArgs e) { Trace.WriteLine(">> CalledLogout called. IsUserLogged:" + sessionVm.IsUserLogged); };
            sessionVm.actionResponse.PropertyChanged += delegate (object s, PropertyChangedEventArgs e) 
            {
                if(e.PropertyName== "responseText")
                    Trace.WriteLine(">> PropertyChanged called. sessionVm.actionResponse.responseText = " + sessionVm.actionResponse.responseText);
            };            
        }

        [TestMethod]
        public void SessionVm_Iniciar_sesión_contraseña_correcta()
        {
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);

            Trace.WriteLine("sessionVm.Login('" + contraseñaCorrectaVieja + "', '" + nombreUsuarioPorDefecto + "')");
            sessionVm.Login(contraseñaCorrectaVieja, nombreUsuarioPorDefecto);
                  
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);
            Assert.IsTrue(sessionVm.IsUserLogged, "Error al inciar sessión.");
        }

        [TestMethod]
        public void SessionVm_Iniciar_sesión_contraseña_incorrecta()
        {
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);

            Trace.WriteLine("sessionVm.Login('" + contraseñaIncorrecta + "', '" + nombreUsuarioPorDefecto + "')");
            sessionVm.Login(contraseñaIncorrecta, nombreUsuarioPorDefecto);
            
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);
            Assert.IsFalse(sessionVm.IsUserLogged, "A pesar de que la contraseña es incorrecta se inició sesión.");
        }

        [TestMethod]
        public void SessionVm_Cambiar_contraseña()
        {
            //Login (+Logout)
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);

            Trace.WriteLine("sessionVm.Login('" + contraseñaCorrectaVieja + "', '" + nombreUsuarioPorDefecto + "')");
            sessionVm.Login(contraseñaCorrectaVieja, nombreUsuarioPorDefecto);       
                 
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);


            //Cambiar contraseña
            Trace.WriteLine("bool e = sessionVm.ChangePasswordCurrentUser('" + contraseñaNueva + "')");
            bool e1 = sessionVm.ChangePasswordCurrentUser(contraseñaNueva);
            
            Assert.IsTrue(e1, "Error al intentar cambiar la contraseña al usuario actual.");

            //Login (+Logout)
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);

            Trace.WriteLine("sessionVm.Login(" + contraseñaNueva + "', '" + nombreUsuarioPorDefecto + "')");
            sessionVm.Login(contraseñaNueva, nombreUsuarioPorDefecto);
            
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);
            Assert.IsTrue(sessionVm.IsUserLogged, "Error al inciar sessión.");

            //Cambiar contraseña a la original
            Trace.WriteLine("bool e = sessionVm.ChangePasswordCurrentUser('" + contraseñaCorrectaVieja + "')");
            bool e2 = sessionVm.ChangePasswordCurrentUser(contraseñaCorrectaVieja);
            
            Assert.IsTrue(e2, "Error al intentar cambiar la contraseña al usuario actual.");

            //Login (+Logout) con la contraseña original
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);

            Trace.WriteLine("sessionVm.Login(" + contraseñaCorrectaVieja + "', '" + nombreUsuarioPorDefecto + "')");
            sessionVm.Login(contraseñaCorrectaVieja, nombreUsuarioPorDefecto);
            
            Trace.WriteLine("\t sessionVm.IsLogged = " + sessionVm.IsUserLogged);
            Assert.IsTrue(sessionVm.IsUserLogged, "Error al inciar sessión.");
        }
    }
}
