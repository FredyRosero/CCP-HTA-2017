using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCP_HTA_2017.ViewModels;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;

namespace CCP_HTA_2017Tests.ViewModels
{
    [TestClass]
    public class AppVm_Registro_Test
    {

        //Arragment        
        ApplicationViewModel appVm = new ApplicationViewModel();
        string contraseñaCorrecta = "simteg";
        string NinExistente = "1";


        public AppVm_Registro_Test() //Suponiendo que tienen como minimo una fila cada una
        {
            appVm.sessionViewModel.SuccessfulLogin += delegate (Object s, EventArgs e) { Trace.WriteLine(">> SuccessfulLogin called."); };
            appVm.sessionViewModel.Login(contraseñaCorrecta);
            Trace.WriteLine("LoadData() >> appVm.pacienteTableViewModel.tableViewModel.Count=" + appVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsFalse(appVm.pacienteTableViewModel.tableViewModel.Count == 0);
            Trace.WriteLine("LoadData() >> appVm.registroTableViewModel.tableViewModel.Count=" + appVm.registroTableViewModel.tableViewModel.Count);
            Assert.IsFalse(appVm.pacienteTableViewModel.tableViewModel.Count == 0);
            // Y si esta vacia la tabla?
        }

        [TestMethod]
        public void AppVm_UpdateTableRegistro()
        {
            appVm.pacienteViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) 
            {
                Trace.WriteLine("( ). appVm.pacienteViewModel.PropertyChanged Called! " + e.PropertyName + " " + e);
            };
            Trace.WriteLine("\tappVm.pacienteTableViewModelSortKeyNin=" + appVm.pacienteTableSortKeyNin);

            appVm.pacienteTableSortKeyNin = NinExistente;
            Trace.WriteLine("appVm.pacienteTableSortKeyNin = NinExistente");

            appVm.PacienteSelectedRowSelectBySortKeyCommand.Execute(null);
            Trace.WriteLine("appVm.PacienteSelectedRowSelectBySortKeyCommand.Execute(null)");

            Trace.WriteLine("\tappVm.pacienteViewModel=" + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.childRegistroTableViewModel.pacienteViewModel.Count)=" + appVm.pacienteViewModel.childRegistroTableViewModel.Count);

            //Busqueda y selección          
            appVm.RegistroSelectedRowChangeToCommand.Execute(appVm.pacienteViewModel.childRegistroTableViewModel[0]);
            Trace.WriteLine("appVm.RegistroSelectedRowChangeToCommand.Execute(appVm.childRegistroTableViewModel[0])");

            Trace.WriteLine("\tappVm.registroViewModel=" + appVm.registroViewModel);

            //Edición
            appVm.registroViewModel.BeginEditCommand.Execute(null);
            Trace.WriteLine("appVm.registroViewModel.BeginEditCommand.Execute(null)");

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow.IsEdit=" + appVm.registroViewModel.rowSelected.IsEdit);

            Trace.WriteLine("appVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"] + ". +1...");
            appVm.registroViewModel.rowSelected["talla"] = (short)appVm.registroViewModel.rowSelected["talla"] + 1;

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"]);

            appVm.registroViewModel.EndEditCommand.Execute(null);
            Trace.WriteLine("appVm.registroViewModel.EndEditCommand.Execute(null)");

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow.IsEdit=" + appVm.registroViewModel.rowSelected.IsEdit);


            Trace.WriteLine("\tappVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"]);
        }



        [TestMethod]
        public void AppVm_RegistroSelectedRowSelectBySortKey_UpdateTableRegistroC()
        {
            // Buscar y obtener
            Trace.WriteLine("\tappVm.registroViewModel=" + appVm.registroViewModel);
            Trace.WriteLine("\tappVm.pacienteTableViewModelSortKeyNin=" + appVm.pacienteTableSortKeyNin);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyFecha=" + appVm.registroTableViewModelSortKeyFecha);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyNinAndFecha=" + appVm.registroTableViewModelSortKeyNinAndFecha);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyNinAndFecha.value=" + appVm.registroTableViewModelSortKeyNinAndFecha.value[0] + ", " + appVm.registroTableViewModelSortKeyNinAndFecha.value[1]);

            appVm.pacienteTableSortKeyNin = NinExistente;
            Trace.WriteLine("\tappVm.pacienteTableSortKeyNin = NinExistente");

            appVm.registroTableViewModelSortKeyFecha = new DateTime(2017, 1, 1);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyFecha = new DateTime(2017, 1, 1)");

            Trace.WriteLine("\tappVm.pacienteTableViewModelSortKeyNin=" + appVm.pacienteTableSortKeyNin);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyFecha=" + appVm.registroTableViewModelSortKeyFecha);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyNinAndFecha=" + appVm.registroTableViewModelSortKeyNinAndFecha);
            Trace.WriteLine("\tappVm.registroTableViewModelSortKeyNinAndFecha.value=" + appVm.registroTableViewModelSortKeyNinAndFecha.value[0] + ", " + appVm.registroTableViewModelSortKeyNinAndFecha.value[1]);

            //Busqueda y selección
            appVm.RegistroSelectedRowSelectBySortKeyCommand.Execute(null);
            Trace.WriteLine("tappVm.RegistroSelectedRowSelectBySortKeyCommand.Execute(null)");

            Trace.WriteLine("\tappVm.registroViewModel=" + appVm.registroViewModel);

            //Edición
            appVm.registroViewModel.BeginEditCommand.Execute(null);
            Trace.WriteLine("tappVm.registroViewModel.BeginEditCommand.Execute(null)");

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow.IsEdit=" + appVm.registroViewModel.rowSelected.IsEdit);

            Trace.WriteLine("appVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"] + ". +1...");
            appVm.registroViewModel.rowSelected["talla"] = (short)appVm.registroViewModel.rowSelected["talla"] + 1;

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"]);

            appVm.registroViewModel.EndEditCommand.Execute(null);
            Trace.WriteLine("tappVm.registroViewModel.EndEditCommand.Execute(null)");

            Trace.WriteLine("\tappVm.registroViewModel.selectedRow.IsEdit=" + appVm.registroViewModel.rowSelected.IsEdit);


            Trace.WriteLine("\tappVm.registroViewModel.selectedRow[talla]=" + appVm.registroViewModel.rowSelected["talla"]);
        }

        public void MyTestCleanup()
        {
            Trace.WriteLine("Se ejecuta?");
        }

    }
}
