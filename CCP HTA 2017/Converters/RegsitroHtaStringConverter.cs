using System;
using System.Globalization;
using System.Windows.Data;

namespace CCP_HTA_2017.Converters
{
    class RegsitroHtaStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DBNull.Value)
                return null;
            else if ((byte)value == 1)
                return "Tipo 1";
            else if ((byte)value == 2)
                return "Tipo 2";
            else if ((byte)value == 3)
                return "Tipo 3";
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Tipo 1")
                return 1;
            else if ((string)value == "Tipo 2")
                return 2;
            else if ((string)value == "Tipo 3")
                return 3;
            else
                return null;
        }
    }
}
