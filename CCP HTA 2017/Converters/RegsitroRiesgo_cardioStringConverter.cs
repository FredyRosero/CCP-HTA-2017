using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CCP_HTA_2017.Converters
{
    class RegsitroRiesgo_cardioStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DBNull.Value)
                return null;
            else if ((byte)value == 1)
                return "Bajo";
            else if ((byte)value == 2)
                return "Medio";
            else if ((byte)value == 3)
                return "Alto";
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Bajo")
                return 1;
            else if ((string)value == "Medio")
                return 2;
            else if ((string)value == "Alto")
                return 3;
            else
                return null;
        }
    }
}
