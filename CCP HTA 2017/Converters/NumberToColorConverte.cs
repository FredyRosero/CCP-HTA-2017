using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CCP_HTA_2017.Converters
{
    
    class NumberToColorConverter : IValueConverter
    {
        byte[] HexStringToByteArray (string hexString)
        {
            byte[] rgb = new byte[3];
            rgb[0] = byte.Parse(hexString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            rgb[1] = byte.Parse(hexString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            rgb[2] = byte.Parse(hexString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            return rgb;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string defaultColor = parameter as string;

            if (value == null)
            {
                return new SolidColorBrush(Colors.Black);
            }
            
            if (value is short)
            {
                value = (int)(short)value;
            }
            if (value is int)
            {
                value = (int)value;
            }
            if (value is long) 
            {
                value = (int)(long)value;
            }
            if (value is decimal) 
            {
                value = (int)(decimal)value;
            }

            float entrada = (float)(int)value;
            Color colorSalida = Colors.Black;
            string[] parameters = new string[5];

            foreach (string stringSetting in Properties.Settings.Default.NumberToColorSetting)
            {
                string[] setting = stringSetting.Split(new char[] { ',' });
                if (setting[0] == parameter.ToString())
                    parameters = setting;
            }

            if (String.IsNullOrEmpty(parameters[0]))
            {
                return new SolidColorBrush(Colors.Black);
            }
            
            float entradaMin = float.Parse(parameters[1]);  // Parameter 1
            string salidaMin = parameters[2];               // Parameter 2
            float entradaMax = float.Parse(parameters[3]);  // Parameter 3
            string salidaMax = parameters[4];               // Parameter 4

            byte[] colorByteMin = HexStringToByteArray(salidaMin);
            byte[] colorByteMax = HexStringToByteArray(salidaMax);

            Color colorMin = Color.FromRgb(colorByteMin[0], colorByteMin[1], colorByteMin[2]);
            Color colorMax = Color.FromRgb(colorByteMax[0], colorByteMax[1], colorByteMax[2]);

            if (entrada >= entradaMax)
            {
                colorSalida = colorMax;
            }
            else if (entrada <= entradaMin)
            {
                colorSalida = colorMin;
            }
            else
            {
                float R2 = colorMax.R;
                float R1 = colorMin.R;
                float Rm = (R2 - R1) / (entradaMax - entradaMin);
                float Rb = R2 - Rm * entradaMax;
                float RSalida = entrada * Rm + Rb;

                float G2 = colorMax.G;
                float G1 = colorMin.G;
                float Gm = (G2 - G1) / (entradaMax - entradaMin);
                float Gb = G2 - Gm * entradaMax;
                float GSalida = entrada * Gm + Gb;

                float B2 = colorMax.B;
                float B1 = colorMin.B;
                float Bm = (B2 - B1) / (entradaMax - entradaMin);
                float Bb = B2 - Bm * entradaMax;
                float BSalida = entrada * Bm + Bb;

                colorSalida = Color.FromScRgb(1.0f,RSalida, GSalida, BSalida);
            }
            return new SolidColorBrush(colorSalida);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
