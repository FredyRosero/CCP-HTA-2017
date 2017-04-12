using CCP_HTA_2017.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CCP_HTA_2017.ViewModels
{
    public class WindowViewModelBase : Window, INotifyPropertyChanged
    {
        //  Estado de la ventana
        #region
        private WindowState _curWindowState = WindowState.Minimized;
        public WindowState CurWindowState
        {
            get { return _curWindowState; }
            set { SetProperty(ref _curWindowState, value); }
        }
        private void MaximizarVentana()
        {
            if (WindowState == WindowState.Maximized)
            {
                ResizeMode = ResizeMode.CanResize;
                WindowState = WindowState.Normal;
            }
            else if (WindowState == WindowState.Normal)
            {
                ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Maximized;

            }
        }
        RelayCommand _MaximizarVentanaCommand; public ICommand MaximizarVentanaCommand
        {
            get
            {
                if (_MaximizarVentanaCommand == null)
                {
                    _MaximizarVentanaCommand = new RelayCommand(param => MaximizarVentana(), param => true);
                }
                return _MaximizarVentanaCommand;
            }
        }
        private void MinimizarVentana()
        {
            ResizeMode = ResizeMode.CanResize;
            WindowState = WindowState.Minimized;
        }
        RelayCommand _MinimizarVentanaCommand; public ICommand MinimizarVentanaCommand
        {
            get
            {
                if (_MinimizarVentanaCommand == null)
                {
                    _MinimizarVentanaCommand = new RelayCommand(param => MinimizarVentana(), param => true);
                }
                return _MinimizarVentanaCommand;
            }
        }
        private void ArrastrarVentana()
        {
            this.DragMove();
        }
        RelayCommand _ArrastrarVentanaCommand; public ICommand ArrastrarVentanaCommand
        {
            get
            {
                if (_ArrastrarVentanaCommand == null)
                {
                    _ArrastrarVentanaCommand = new RelayCommand(param => ArrastrarVentana(), param => true);
                }
                return _ArrastrarVentanaCommand;
            }
        }

        RelayCommand _CerrarAplicaciónCommand; public ICommand CerrarAplicaciónCommand
        {
            get
            {
                if (_CerrarAplicaciónCommand == null)
                {
                    _CerrarAplicaciónCommand = new RelayCommand(param => Application.Current.Shutdown(), param => true);
                }
                return _CerrarAplicaciónCommand;
            }
        }

        #endregion //  Estado de la ventana
        #region Interfaz INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion Interfaz INotifyPropertyChanged
    }
}
