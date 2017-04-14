using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CCP_HTA_2017.Controls
{
    /// <summary>
    /// Interaction logic for ButtonIcon.xaml
    /// </summary>
    public partial class ButtonIcon : UserControl
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(ButtonIcon), new PropertyMetadata(null));
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(ButtonIcon), new PropertyMetadata(null) );
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        #region Command
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(ButtonIcon), 
            new PropertyMetadata(null, OnCommandChanged)
        );
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private static void OnCommandChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var buttonIcon = (ButtonIcon)dependencyObject;

            var _command = args.OldValue as ICommand;
            if (_command != null)
                _command.CanExecuteChanged -= buttonIcon.CommandOnCanExecuteChanged;

            _command = args.NewValue as ICommand;
            if (_command != null)
                _command.CanExecuteChanged += buttonIcon.CommandOnCanExecuteChanged;
        }

        //[DebuggerStepThrough]
        private void CommandOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = Command.CanExecute(null);
        }

        #endregion Command

        #region CommandParameter

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(Object),
            typeof(ButtonIcon),
            new PropertyMetadata(null)
        );

        public Object CommandParameter
        {
            get { return (Object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion CommandParameter

        public ButtonIcon()
        {
            InitializeComponent();
        }

        private void CommandExecute(object sender, MouseButtonEventArgs e)
        {
            if (Command!=null) Command.Execute(null);
        }
    }
}
