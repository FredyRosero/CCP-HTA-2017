using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CCP_HTA_2017.Controls
{
    /// <summary>
    /// Interaction logic for ButtonIcon.xaml
    /// </summary>
    public partial class ButtonIcon : UserControl
    {
        public static readonly DependencyProperty DataPropery = DependencyProperty.Register("Data", typeof(Geometry), typeof(ButtonIcon), new PropertyMetadata(null));
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataPropery); }
            set { SetValue(DataPropery, value); }
        }
        public static readonly DependencyProperty FillPropery = DependencyProperty.Register("Fill", typeof(Brush), typeof(ButtonIcon), new PropertyMetadata(null) );
        public Brush Fill
        {
            get { return (Brush)GetValue(FillPropery); }
            set { SetValue(FillPropery, value); }
        }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public ButtonIcon()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Command.Execute(CommandParameter);
        }
    }
}
