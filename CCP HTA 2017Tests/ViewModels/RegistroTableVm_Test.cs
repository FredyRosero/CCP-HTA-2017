using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.ViewModels;
using System.Diagnostics;
using System.Data;

namespace CCP_HTA_2017Tests.ViewModels
{
    [TestClass]
    public class RegistroTableVm_Test
    {
        //Aplicacion Alias
        public RegistroTableViewModel registroTableViewModel;
        public ApplicationDataAccess applicationDataAccess = new ApplicationDataAccess();
        string pacienteTableViewModelSortKeyNin = "1";
        DateTime registroTableViewModelSortKeyFecha = new DateTime(2017, 1, 1); // registro[1][fecha]1/1/2017
        RegistroTableViewModel.RegistroSortKey registroTableViewModelSortKeyValue;
        public RegistroTableVm_Test()
        {
            applicationDataAccess.pacienteTableAdapter.Fill(applicationDataAccess.dataSet.paciente);
            applicationDataAccess.registroTableAdapter.Fill(applicationDataAccess.dataSet.registro);
            registroTableViewModel = new RegistroTableViewModel(applicationDataAccess);
        }

        [TestMethod]
        public void RegistroTable_Indice_Cero()
        {
            Trace.WriteLine("registroTableViewModel.tableViewModel[0]=[fecha=" + registroTableViewModel.tableViewModel[0]["fecha"] +", nin=" + registroTableViewModel.tableViewModel[0]["paciente_nin"] + "]");
            Trace.WriteLine("registroTableViewModelSortKeyValue==registroTableViewModel.tableViewModel[0][fecha]?=" + ((DateTime)registroTableViewModel.tableViewModel[0]["fecha"] == registroTableViewModelSortKeyFecha) );
        }


        [TestMethod]
        public void RegistroTableViewModel_tableViewModel_Find()
        {
            registroTableViewModelSortKeyValue = new RegistroTableViewModel.RegistroSortKey(
                                                    pacienteTableViewModelSortKeyNin,
                                                    registroTableViewModelSortKeyFecha );
            Trace.WriteLine("\tregistroTableViewModelSortKeyValue.value=" + registroTableViewModelSortKeyValue.value[0] + ", " + registroTableViewModelSortKeyValue.value[1]);

            int rowIndex = registroTableViewModel.tableViewModel.Find( new object[] { pacienteTableViewModelSortKeyNin, registroTableViewModelSortKeyFecha } );
            Trace.WriteLine("registroTableViewModel.tableViewModel.Find( new object[] { pacienteTableViewModelSortKeyNin, registroTableViewModelSortKeyFecha } )=" + rowIndex);

            DataRowView registro = registroTableViewModel.FindRowBySortKey(registroTableViewModelSortKeyValue);
            Trace.WriteLine("registroTableViewModel.FindRegistro(registroTableViewModelSortKeyValue)=" + registro);
        }
    }
}
