using System;
using System.Windows.Input;
using CCP_HTA_2017.Bussines;
using CCP_HTA_2017.Commands;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.Models;

namespace CCP_HTA_2017.ViewModels
{
    /// <summary>
    /// Permite iniciar sesión mediante autenticación y añadir un evento al logueo.
    /// </summary>
    public class SessionViewModel : ResponseViewModelBase
    {
        /// <summary>Modelo de datos de la sesión.</summary>
        private SessionModel sessionModel;
        
        /// <summary>View model de la tabla de usuarios.</summary>
        private UsuarioTableViewModel usuarioTableViewModel;

        /// <summary>Si se ha iniciado sesión.  </summary>
        public bool IsUserLogged
        {
            get { return sessionModel.m_isLogged; }
            set { SetProperty(ref sessionModel.m_isLogged, value); }
        }

        /// <summary>EventHandler para llamar luego de inicio de sesión exitoso.</summary>
        public event EventHandler SuccessfulLogin;
        /// <summary>EventHandler para llamar luego de cierre de sesión.</summary>
        public event EventHandler CalledLogout;
        
        #region Login
        /// <summary>  </summary>
        public bool Login(string contraseña, string usuario="default")
        {
            if (IsUserLogged) Logout();
            var success = false;
            if (usuarioTableViewModel.compararContraseña(contraseña, usuario))
            {
                IsUserLogged = true;
                sessionModel.userName = "default";
                OnSuccessfulLogin();
                success = true;
                actionResponse.Set("Inicio de sesión exitoso", ResponseType.Success);
                RegistroActividad.escribirActividad("Inicio de sesión exitoso");
            } 
            else
            {
                actionResponse.Set("Inicio de sesión fallido", ResponseType.Error);
                RegistroActividad.escribirActividad("Inicio de sesión fallido");
            }
            return success;
        }

        void LoginCommandExecute(object param)
        {
            object[] loginParameters = param as object[];
            Login(loginParameters[0].ToString(), "default");
        }

        RelayCommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand(param => LoginCommandExecute(param), param => true);
                }
                return _LoginCommand;
            }
        }

        /// <summary> Se llama cuando el inicio de sesión ha sido exitoso. </summary>
        protected virtual void OnSuccessfulLogin()
        {
            SuccessfulLogin?.Invoke(null, EventArgs.Empty);
        }
        #endregion Login

        #region Logout
        /// <summary>  </summary>
        public void Logout ()
        {
            sessionModel.userName = null;
            OnLogout();
            actionResponse.Set("Cierre de sesión.", ResponseType.Success);
            RegistroActividad.escribirActividad("Cierre de sesión");
        }

        /// <summary> Se llama cuando el inicio de sesión ha sido exitoso. </summary>
        protected virtual void OnLogout()
        {
            CalledLogout?.Invoke(null, EventArgs.Empty);
        }
        RelayCommand _LogoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (_LogoutCommand == null)
                {
                    _LogoutCommand = new RelayCommand(param => Logout(), param => true);
                }
                return _LogoutCommand;
            }
        }
        #endregion Logout

        #region ChangePasswordCurrentUser
        /// <summary>  </summary>
        public bool ChangePasswordCurrentUser(string contraseña)
        {
            if (sessionModel.userName == null)
            {
                actionResponse.Set("No hay una sesión activa.", ResponseType.Error);
                return false;
            }
            else if (usuarioTableViewModel.actualizarContraseña(contraseña, sessionModel.userName))
            {
                actionResponse.Set("Exito al cambiar la contraseña.", ResponseType.Success);
                return true;                
            }
            actionResponse.Set("Error al intentar cambiar la contraseña.", ResponseType.Error);
            return false;
        }

        void ChangePasswordCurrentUserCommandExecute(object param)
        {
            ChangePasswordCurrentUser(param.ToString());
        }

        RelayCommand _ChangePAsswordCurrentUserCommand;
        public ICommand ChangePAsswordCurrentUserCommand
        {
            get
            {
                if (_ChangePAsswordCurrentUserCommand == null)
                {
                    _ChangePAsswordCurrentUserCommand = new RelayCommand(param => ChangePasswordCurrentUserCommandExecute(param), param => true);
                }
                return _ChangePAsswordCurrentUserCommand;
            }
        }
        #endregion ChangePasswordCurrentUser

        /// <summary>  </summary>
        public SessionViewModel (ApplicationDataAccess applicationDataAccess)
        {
            sessionModel = new SessionModel() { userName = "default" };
            usuarioTableViewModel = new UsuarioTableViewModel(applicationDataAccess);
        }
    }
}
