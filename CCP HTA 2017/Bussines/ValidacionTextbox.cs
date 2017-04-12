using System.Windows.Controls;

namespace CCP_HTA_2017.Bussines
{
    public class ValidacionTextbox : ValidationRule
    {
        public override ValidationResult Validate
        (object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "El valor no puede estar vacio.");
            if (value.ToString() == "") return new ValidationResult(false, "El valor no puede estar vacio.");
            if (value.ToString() == null) return new ValidationResult(false, "Escoja una opción válida.");
            return ValidationResult.ValidResult;
        }
    }
}

