using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP_HTA_2017.Bussines
{
    class ColumnChangeValidationRules
    {
        public static void PacienteColumnChangedValitadion(object sender, DataColumnChangeEventArgs e)
        {
            //e.Row.SetColumnError
            System.Diagnostics.Debug.WriteLine("Column_Changed Event: name={0}; Column={1}; original name={2}",
                e.Row["name"], e.Column.ColumnName, e.Row["name", DataRowVersion.Original]);
        }
    }
}
