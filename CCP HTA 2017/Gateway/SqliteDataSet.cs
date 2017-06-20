using CCP_HTA_2017.Bussines;
using System;
using System.Data;

namespace CCP_HTA_2017.Gateway
{
    public partial class SqliteDataSet
    {
        partial class registroDataTable
        {
        }

        partial class configuracionDataTable
        {
        }

        partial class pacienteDataTable
        {
            public void SetValidationRules()
            {
                ColumnChanged += new DataColumnChangeEventHandler(ColumnChangeValidationRules.PacienteColumnChangedValitadion);
            }

        }
    }
}






