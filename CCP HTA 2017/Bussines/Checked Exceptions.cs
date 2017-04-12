using System;

namespace CCP_HTA_2017.Bussines
{
    public class LongituNinEncimaDe10Exception : Exception
    {
        public override string Message
        {
            get { return "El parámetro 'nin' no debe ser mayor de 10 caráctees."; }
        }
    }

    public class CarácterNinAlfanumericoException : Exception
    {
        public override string Message
        {
            get { return "El parámetro 'nin' no debe poseer carácteres alfanuméricos."; }
        }
    }

    public class PacientePorNinNoEncontrado : Exception
    {
        public override string Message
        {
            get { return "No exite un paciente con el 'nin' específicado."; }
        }
    }
}
