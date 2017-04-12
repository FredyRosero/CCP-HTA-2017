using CCP_HTA_2017.Gateway;
using System;
using System.Data;

namespace CCP_HTA_2017.ViewModels
{
    public class RegistroTableViewModel : TableViewModelBase
    {
        public class RegistroSortKey
        {
            string nin { get; set; }
            DateTime? fecha { get; set; }
            public RegistroSortKey (string nin, DateTime fecha)
            {
                this.nin = nin;
                this.fecha = fecha; 
            }
            public object[] value
            {
                get { return new object[] { this.nin, this.fecha }; }
            }
            public override string ToString()
            {
                return "[" + nin + ", " + fecha==null? "Null Date" : ((DateTime)fecha).ToString("dd/MM/yyyy") + "]";
            }
        }
        //Find RowView: (Paciente Table Model)
        /// <summary>Busca y devuelve el <see cref="DataRowView"/> del registro que corresponde al 'nin' y fecha específicado especídicado, En la <see cref="DataView"/> de registros actual. </summary>
        /// <returns> Return Value: <see cref="DataRowView"/> La fila que contiene al paciente con el 'nin' especificado. /// </returns>
        /// <param name="nin"> El Número de Identificación Nacional (NIN) del paciente asociado al registro. </param>
        /// <param name="fecha"> La fecha del registro. </param>
        override public DataRowView FindRowBySortKey(object sortKey)
        {
            var registroSortKey = sortKey as RegistroSortKey;
            int rowIndex = tableViewModel.Find(registroSortKey.value);    // DataView.Find (Object): https://msdn.microsoft.com/es-es/library/46d41xk2(v=vs.110).aspx 
                                                                  // The index of the row in the System.Data.DataView that contains the sort key value
                                                                  // specified; otherwise -1 if the sort key value does not exist.

            if (rowIndex != -1)
                return tableViewModel[rowIndex];
            else
                return null;
        }

        /// <summary> . </summary>
        override public void UpdateTable(bool LookCurrentRowViewModel = false)
        {
            UpdateTableAdapter(TableAdapter.registro, LookCurrentRowViewModel);
        }

        //Contructor
        /// <summary> .Inherit contructor of  <see cref="TableViewModelBase"/> </summary>
        public RegistroTableViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess)
        {
            this.tableViewModel = applicationDataAccess.dataSet.registro.DefaultView;
            tableViewModel.Sort = "paciente_nin ASC, fecha DESC";
        }
    }
}
