using CCP_HTA_2017.Bussines;
using CCP_HTA_2017.Gateway;
using System;
using System.Data;
using System.Linq;

namespace CCP_HTA_2017.ViewModels
{

    public class PacienteTableViewModel : TableViewModelBase
    {
        //Find RowView: (Paciente Table Model)
        /// <summary>Busca y devuelve el <see cref="DataRowView"/> del paciente que corresponde al 'nin' especídicado, En la <see cref="DataView"/> de pacientes actual. </summary>
        /// <returns> Return Value: <see cref="DataRowView"/> La fila que contiene al paciente con el 'nin' especificado. /// </returns>
        /// <param name="pacienteSortKey"> El Número de Identificación Nacional (NIN) por el cual se buscará al paciente. </param>
        override public DataRowView FindRowBySortKey(object sortKey)
        {
            var pacienteSortKey = sortKey as string;
            if (pacienteSortKey == null)
                throw new ArgumentNullException("nin");

            if (pacienteSortKey.Length > 10)
                throw new LongituNinEncimaDe10Exception();

            if (pacienteSortKey.Any(x => char.IsLetter(x)))
                throw new CarácterNinAlfanumericoException();

            int rowIndex = tableViewModel.Find(pacienteSortKey); // The index of the row in the System.Data.DataView that contains the sort key value
                                                     // specified; otherwise -1 if the sort key value does not exist.

            if (rowIndex != -1)
            {
                if (HasNewRow)
                    CurrentRowViewModel.rowSelected.CancelEdit();
                return tableViewModel[rowIndex];
            }
            return null;
        }

        /// <summary> . </summary>
        override public void UpdateTable(bool LookCurrentRowViewModel = false)
        {
                UpdateTableAdapter(TableAdapter.paciente, LookCurrentRowViewModel);
        }

        //Contructor
        /// <summary> .Inherit contructor of  <see cref="TableViewModelBase"/> </summary>
        public PacienteTableViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess)
        {
            this.tableViewModel = applicationDataAccess.dataSet.paciente.DefaultView;
            tableViewModel.Sort = "nin";            
        }

    }
}
