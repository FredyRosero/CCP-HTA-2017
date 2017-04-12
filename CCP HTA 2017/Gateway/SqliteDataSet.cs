using CCP_HTA_2017.Bussines;
using System;
using System.Data;

namespace CCP_HTA_2017.Gateway
{
    public partial class SqliteDataSet
    {

        partial class pacienteDataTable
        {
            public void SetValidationRules()
            {
                ColumnChanged += new DataColumnChangeEventHandler(ColumnChangeValidationRules.PacienteColumnChangedValitadion);
            }

        }
    }
}






