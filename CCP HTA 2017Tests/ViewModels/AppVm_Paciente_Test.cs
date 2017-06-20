using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCP_HTA_2017.ViewModels;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;

namespace CCP_HTA_2017Tests.ViewModels
{
    [TestClass]
    public class AppVm_Paciente_Test
    {
        //Arragment        
        ApplicationViewModel appVm = new ApplicationViewModel();
        string contraseñaCorrecta = "simteg";
        string NinExistente = "1";

        public AppVm_Paciente_Test() //Suponiendo que tienen como minimo una fila cada una
        {
            appVm.sessionViewModel.SuccessfulLogin += delegate (Object s, EventArgs e) { Trace.WriteLine("SuccessfulLogin called."); };
            appVm.sessionViewModel.actionResponse.PropertyChanged += delegate (object s, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "responseText")
                    Trace.WriteLine(">> PropertyChanged called. sessionVm.actionResponse.responseText = " + appVm.sessionViewModel.actionResponse.responseText);
            };

            Trace.WriteLine("appVm.sessionViewModel.Login(contraseñaCorrecta)");
            appVm.sessionViewModel.Login(contraseñaCorrecta);

            /* conteo de filas */
            Trace.WriteLine("LoadData() >> appVm.pacienteTableViewModel.tableViewModel.Count=" + appVm.pacienteTableViewModel.tableViewModel.Count);
            Assert.IsFalse(appVm.pacienteTableViewModel.tableViewModel.Count == 0);
            Trace.WriteLine("LoadData() >> appVm.registroTableViewModel.tableViewModel.Count=" + appVm.registroTableViewModel.tableViewModel.Count);
            Assert.IsFalse(appVm.pacienteTableViewModel.tableViewModel.Count == 0);
            // Y si esta vacia la tabla?

            appVm.pacienteTableViewModel.onUpdateTableEventHandler += delegate (object s, OnUpdateTableEventArgs e)
            {
                Trace.WriteLine(">> onUpdateTableEventHandler called...");
                Trace.WriteLine("\t>> appVm.pacienteViewModel.actionResponse.responseText = " + appVm.pacienteViewModel.actionResponse.responseText);
                Trace.WriteLine("\t>> appVm.pacienteTableViewModel.actionResponse.responseText = " + appVm.pacienteTableViewModel.actionResponse.responseText);
            };

            appVm.pacienteTableViewModel.tableViewModel.ListChanged 
                += new System.ComponentModel.ListChangedEventHandler(OnListChanged);

            //appVm.pacienteViewModel.rowSelected.PropertyChanged
            //    += new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChange);
        }

        private void OnListChanged(object sender, System.ComponentModel.ListChangedEventArgs args)
        {
            Trace.WriteLine("\tListChanged:");
            Trace.WriteLine("\t\ttable    Type = " + args.ListChangedType);
            Trace.WriteLine("\t\tldIndex = " + args.OldIndex);
            Trace.WriteLine("\t\tNewIndex = " + args.NewIndex);
        }

        private void OnPropertyChange(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            Trace.WriteLine("\tPropertyChange: property name: " + args.PropertyName);
        }

        [TestMethod]
        public void AppVm_PacienteSelectedRowNewAdd() //Create selection
        {
            Trace.WriteLine("AppVm_PacienteSelectedRowNewAdd():");
            //**/
            appVm.PacienteSelectedRowNewAddCommand.Execute(null);
            Trace.WriteLine("appVm.PacienteSelectedRowSelectBySortKeyCommand.Execute(null);");

            Trace.WriteLine("\tappVm.pacienteViewModel=" + appVm.pacienteViewModel);
        }

        [TestMethod]
        public void AppVm_PacienteSelectedRowSelectBySortKey() // Read paciente
        {
            Trace.WriteLine("ATTEMP: appVm.pacienteTableViewModelSortKeyNin = " + NinExistente);
            appVm.pacienteTableSortKeyNin = NinExistente;
            Trace.WriteLine("appVm.pacienteTableViewModelSortKeyNin = " + NinExistente);

            //**/
            appVm.PacienteSelectedRowSelectBySortKeyCommand.Execute(null);
            Trace.WriteLine("appVm.PacienteSelectedRowSelectBySortKeyCommand.Execute(null);");

            Trace.WriteLine("\tappVm.pacienteViewModel=" + appVm.pacienteViewModel);
        }



        [TestMethod]
        public void AppVm_PacienteSelectedRowNewAdd_UpdateTablePaciente_Imcompleto() //Update
        {
            //-------------
            Trace.WriteLine("/* Agregar nuevo paciente: */");
            Trace.WriteLine("appVm.PacienteSelectedRowNewAddCommand.Execute(null);");
            appVm.PacienteSelectedRowNewAddCommand.Execute(null);

            Trace.WriteLine("NuevoPaciente = appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected; = " + appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected);
            DataRowView NuevoPaciente = appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected;

            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel.Count = " + appVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel..Table.Rows.Count = " + appVm.pacienteTableViewModel.tableViewModel.Table.Rows.Count);
            Trace.WriteLine("\tappVm.pacienteViewModel = " + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow.IsEdit = " + appVm.pacienteViewModel.rowSelected.IsEdit);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.HasNewRow = " + appVm.pacienteTableViewModel.HasNewRow);
                        
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow['nombre'] =" + appVm.pacienteViewModel.rowSelected["nombre"]);            
            
            //-------------
            Trace.WriteLine("/* Cambiar propiedad del paciente: */"); 

            Trace.WriteLine("Set. appVm.pacienteViewModel.selectedRow['nombre'] = " + appVm.pacienteViewModel.rowSelected["nombre"]);
            appVm.pacienteViewModel.rowSelected["nombre"] = appVm.pacienteViewModel.rowSelected["nombre"] + "N";
            
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow['nombre'] = " + appVm.pacienteViewModel.rowSelected["nombre"]);
            
            //-------------
            Trace.WriteLine("/* Finalizar la edición: */");
            Trace.WriteLine("Try#1. appVm.pacienteViewModel.EndEditCommand.Execute(null)");
            try
            {
                appVm.pacienteViewModel.EndEditCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception#1: " + ex + ". " + ex.Message);
            }

            Trace.WriteLine("\tappVm.pacienteViewModel = " + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow.IsEdit = " + appVm.pacienteViewModel.rowSelected.IsEdit);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.HasNewRow = " + appVm.pacienteTableViewModel.HasNewRow);
            

            //--------------
            Trace.WriteLine("/* Actualizar la tabla: */"); 
            Trace.WriteLine("Try#2. appVm.pacienteTableViewModel.UpdateTableCommand.Execute(null);");                        
            try
            {
                appVm.pacienteTableViewModel.UpdateTableCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception#2: " + ex + ". " + ex.Message);
            }
        }


        [TestMethod]
        public void AppVm_PacienteSelectedRowNewAdd_UpdateTablePaciente_Completo() //Update
        {
            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel[0].Row.RowState = " + appVm.pacienteTableViewModel.tableViewModel[0].Row.RowState);

            //-------------
            Trace.WriteLine("/* Agregar nuevo paciente: */");
            Trace.WriteLine("appVm.PacienteSelectedRowNewAddCommand.Execute(null);");
            appVm.PacienteSelectedRowNewAddCommand.Execute(null);

            Trace.WriteLine("NuevoPaciente = appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected; = " + appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected);
            DataRowView NuevoPaciente = appVm.pacienteTableViewModel.CurrentRowViewModel.rowSelected;

            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel.Count = " + appVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel..Table.Rows.Count = " + appVm.pacienteTableViewModel.tableViewModel.Table.Rows.Count);
            Trace.WriteLine("\tappVm.pacienteViewModel = " + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow.IsEdit = " + appVm.pacienteViewModel.rowSelected.IsEdit);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.HasNewRow = " + appVm.pacienteTableViewModel.HasNewRow);
            
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow['nombre'] =" + appVm.pacienteViewModel.rowSelected["nombre"]);

            //-------------           
            Trace.WriteLine("/* Cambiar todas las propiedades correctamente: */");
            appVm.pacienteViewModel.rowSelected["nin"] = 99;
            appVm.pacienteViewModel.rowSelected["nombre"] = "test";
            appVm.pacienteViewModel.rowSelected["apellido"] = "test";
            appVm.pacienteViewModel.rowSelected["nacimiento"] = DateTime.Today;
            appVm.pacienteViewModel.rowSelected["hombre"] = 1;

            Trace.WriteLine("\tappVm.pacienteViewModel = " + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow.IsEdit = " + appVm.pacienteViewModel.rowSelected.IsEdit);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.HasNewRow = " + appVm.pacienteTableViewModel.HasNewRow);
            
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow['nombre'] = " + appVm.pacienteViewModel.rowSelected["nombre"]);

            //--------------
            Trace.WriteLine("/* Actualizar la tabla: */");
            Trace.WriteLine("Try#3. appVm.pacienteTableViewModel.UpdateTableCommand.Execute(null);");
            try
            {
                appVm.pacienteTableViewModel.UpdateTableCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception#3: " + ex + ". " + ex.Message);
            }

            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel.Count = " + appVm.pacienteTableViewModel.tableViewModel.Count);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.tableViewModel..Table.Rows.Count = " + appVm.pacienteTableViewModel.tableViewModel.Table.Rows.Count);
            Trace.WriteLine("\tappVm.pacienteViewModel = " + appVm.pacienteViewModel);
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow.IsEdit = " + appVm.pacienteViewModel.rowSelected.IsEdit);
            Trace.WriteLine("\tappVm.pacienteTableViewModel.HasNewRow = " + appVm.pacienteTableViewModel.HasNewRow);
            
            Trace.WriteLine("\tappVm.pacienteViewModel.selectedRow['nombre'] =" + appVm.pacienteViewModel.rowSelected["nombre"]);
        }
    }
}
