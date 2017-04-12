using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CCP_HTA_2017.Converters
{
    class PacienteNacimientoFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            DateTime nacimiento = (DateTime)value;
            return (float)DateTime.Now.Year + (float)DateTime.Now.Month / 12 - (float)nacimiento.Year - (float)nacimiento.Month / 12;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
