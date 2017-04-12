using System;
using System.Globalization;
using System.Windows.Data;

namespace CCP_HTA_2017.Converters
{
    class RegsitroHombreStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value== DBNull.Value)
                return null;
            else if ((byte)value == 1)
                return "Hombre";
            else
                return "Mujer";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Hombre")
                return 1;
            else if ((string)value == "Mujer")
                return 0;
            else
                return null;
        }
    }
}
