using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CCP_HTA_2017.ViewModels;
using CCP_HTA_2017.Commands;
using System.Windows.Controls.Primitives;

namespace CCP_HTA_2017.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : WindowViewModelBase
    {
        ApplicationViewModel applicationViewModel = App.applicationViewModel;

        //Windows
        #region Windows

        // cambiarContraseña
        #region
        private void CambioContraseñaWindowShow()
        {
            CambioContraseñaWindow cambiarContraseña = new CambioContraseñaWindow();
            cambiarContraseña.Show();
        }
        RelayCommand _CambioContraseñaWindowShowCommand;
        public ICommand CambioContraseñaWindowShowCommand
        {
            get
            {
                if (_CambioContraseñaWindowShowCommand == null)
                {
                    _CambioContraseñaWindowShowCommand = new RelayCommand(param => CambioContraseñaWindowShow(), param => true);
                }
                return _CambioContraseñaWindowShowCommand;
            }
        }
        #endregion //cambiarContraseña

        // GestorTablaRegistroWindow
        #region
        public void GestorTablaRegistroWindowShow()
        {
            GestorTablaRegistroWindow GestorTablaRegistroWindow = new GestorTablaRegistroWindow();
            GestorTablaRegistroWindow.Show();
        }
        RelayCommand _GestorTablaRegistroWindowShowCommand; public ICommand GestorTablaRegistroWindowShowCommand
        {
            get
            {
                if (_GestorTablaRegistroWindowShowCommand == null)
                {
                    _GestorTablaRegistroWindowShowCommand = new RelayCommand(param => GestorTablaRegistroWindowShow(), param => true);
                }
                return _GestorTablaRegistroWindowShowCommand;
            }
        }
        #endregion //GestorTablaRegistroWindow

        // GestorTablaPacienteWindow
        #region
        public void GestorTablaPacienteWindowShow()
        {
            GestorTablaPacienteWindow GestorTablaPacienteWindow = new GestorTablaPacienteWindow();
            GestorTablaPacienteWindow.Show();
        }
        RelayCommand _GestorTablaPacienteWindowShowCommand; public ICommand GestorTablaPacienteWindowShowCommand
        {
            get
            {
                if (_GestorTablaPacienteWindowShowCommand == null)
                {
                    _GestorTablaPacienteWindowShowCommand = new RelayCommand(param => GestorTablaPacienteWindowShow(), param => true);
                }
                return _GestorTablaPacienteWindowShowCommand;
            }
        }
        #endregion //GestorTablaPacienteWindow 

        #endregion Windows

        //animación
        #region Animación
        static private readonly SineEase EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut };
        static private readonly Duration AnimationDuration = TimeSpan.FromMilliseconds(500);
        
        //Paciente animation
        #region Paciente animation
        static private double PacienteMaxHeightTemp;        
        DoubleAnimation PacienteExpand = new DoubleAnimation()
        {
            Duration = AnimationDuration,
            FillBehavior = FillBehavior.HoldEnd,
            EasingFunction = EasingFunction,
            From = 0.0,
        };
        public void PacienteExpandBeginAnimation()
        {
            if (pacienteWrapPanel.MaxHeight == 0)
            {
                PacienteExpand.To = PacienteMaxHeightTemp;
                pacienteWrapPanel.BeginAnimation(WrapPanel.MaxHeightProperty, PacienteExpand);
            }      
        }
        DoubleAnimation PacienteColapsar = new DoubleAnimation()
        {
            Duration = AnimationDuration,
                FillBehavior = FillBehavior.HoldEnd,
                EasingFunction = EasingFunction,
                To = 0.0
            };
        public void PacienteColapseBeginAnimation()
        {            
            if (pacienteWrapPanel.MaxHeight != 0.0)
            {
                PacienteColapsar.From = PacienteMaxHeightTemp;
                pacienteWrapPanel.BeginAnimation(WrapPanel.MaxHeightProperty, PacienteColapsar);
            }
        }
        private void InitializePacienteAnimation ()
        {
            PacienteExpand.Completed += (s, e) =>
            {
                UIPaciente_RowDefinition.Height = GridLength.Auto;
                PacienteColapsar.From = pacienteWrapPanel.ActualHeight;
            };
            PacienteColapsar.Completed += (s, e) => 
            {
                UIPaciente_RowDefinition.Height = GridLength.Auto;
            };
            /* Inicializar FromToMaxHeightWrapPanel (medir ) http://stackoverflow.com/a/1695821/2468217 */
            Loaded += delegate
            {                
                IEnumerable<FrameworkElement> pacienteWrapPanelChildren = pacienteWrapPanel.Children.OfType<FrameworkElement>();
                foreach (FrameworkElement Control in pacienteWrapPanelChildren)                
                    PacienteMaxHeightTemp += Control.ActualHeight + Control.Margin.Top + Control.Margin.Bottom;                
            };
        }
        #endregion Paciente animation

        //Regsitro animación
        #region Regsitro animación
        static private double RegistroWidthTemp;
        DoubleAnimation RegistroExpand = new DoubleAnimation()
        {
            Duration = AnimationDuration,
            FillBehavior = FillBehavior.Stop,
            EasingFunction = EasingFunction,
            From = 0.0,
        };
        public void RegistroExpandBeginAnimation()
        {            
            if (UIRegistro_Grid.Width == 0)
            {                
                UIRegistro_Grid.BeginAnimation(Grid.WidthProperty, RegistroExpand);
            }
        }
        DoubleAnimation RegistroColapse = new DoubleAnimation()
        {
            Duration = AnimationDuration,
            FillBehavior = FillBehavior.Stop,
            EasingFunction = EasingFunction,
            To = 0.0
        };
        public void RegistroColapseBeginAnimation()
        {
            if (UIRegistro_Grid.Width != 0)
            {
                RegistroExpand.To = UIRegistro_Grid.Width;
                RegistroColapse.From = UIRegistro_Grid.Width;
                UIRegistro_Grid.BeginAnimation(Grid.WidthProperty, RegistroColapse);
            }
        }
        private void InitializeRegistroAnimation()
        {
            RegistroWidthTemp = UIRegistro_Grid.Width;
            RegistroExpand.Completed += (s, e) =>
            {
                UIRegistro_Grid.Width = (double)RegistroExpand.To;                
            };
            RegistroColapse.Completed += (s, e) =>
            {
                UIRegistro_Grid.Width = (double)RegistroColapse.To;
            };
            Loaded += delegate 
            {
                RegistroExpand.To = UIRegistro_Grid.Width;
            };
        }
        private void LinkColumnWithGridWidth()
        {
            UIRegistro_GridSplitter.DragDelta += delegate (object s, DragDeltaEventArgs e)
            {
                var x = UIRegistro_ColumnDefinition.ActualWidth;
                UIRegistro_Grid.Width = x;
            };
            UIRegistro_GridSplitter.DragCompleted += delegate (object s, DragCompletedEventArgs e)
            {
                UIRegistro_ColumnDefinition.Width = GridLength.Auto;
            };
        }
        #endregion Regsitro animación

        #endregion Animación

        public MainWindow()
        {            
            InitializeComponent();

            // Inicalización de las animaciones
            LinkColumnWithGridWidth();
            InitializeRegistroAnimation();
            InitializePacienteAnimation();

            

            // 
            applicationViewModel.pacienteViewModel.OnSetRowSelectedEventHandler += delegate (object sender, OnSetRowSelectedEventArgs args)
            {
                if (args.dataRowView == null)
                    PacienteColapseBeginAnimation();
                else
                    PacienteExpandBeginAnimation();
            };

            applicationViewModel.registroViewModel.OnSetRowSelectedEventHandler += delegate (object sender, OnSetRowSelectedEventArgs args)
            {
                if (args.dataRowView == null)
                    RegistroColapseBeginAnimation();
                else
                    RegistroExpandBeginAnimation();
            };

        }
    }
}



