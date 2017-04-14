using System;
using CCP_HTA_2017.Gateway;
using System.Data;
using CCP_HTA_2017.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;

namespace CCP_HTA_2017.ViewModels
{
    public partial class ApplicationViewModel : ResponseViewModelBase
    {
        //Data Access:
        public bool IsDataLoaded = false; // (Binding data) Can change --> Notify Property Change...
        public ApplicationDataAccess applicationDataAccess = new ApplicationDataAccess();
        
        /// <summary> . </summary>
        public void LoadData(object sender, EventArgs e)
        {
            try
            {
                applicationDataAccess.pacienteTableAdapter.Fill(applicationDataAccess.dataSet.paciente);
                //PacienteSelectedRowNewAdd(); // Se debe agregar la fila nueva, despues de cargar los datos, por el método 'Fill' cancela resetea la tabla
                applicationDataAccess.registroTableAdapter.Fill(applicationDataAccess.dataSet.registro);
                //RegistroSelectedRowNewAdd(); // Se debe agregar la fila nueva, despues de cargar los datos, por el método 'Fill' cancela resetea la tabla
                IsDataLoaded = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary> . </summary>
        public void ClearData(object sender, EventArgs e)
        {
            try
            {
                applicationDataAccess.dataSet.Clear();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //ViewModels:

        /// <summary> Sessión actual en la aplicación </summary>
        public SessionViewModel sessionViewModel { set; get; } // (DataContext)


        //Contructur
        /// <summary> . </summary>
        public ApplicationViewModel ()
        {
            // Sesión
            sessionViewModel = new SessionViewModel(applicationDataAccess);
            sessionViewModel.SuccessfulLogin += LoadData;
            sessionViewModel.CalledLogout += ClearData;

            // Paciente
            pacienteTableViewModel = new PacienteTableViewModel(applicationDataAccess);

            /* La fila del paciente no se actualiza/guarda desde el Vista de modelo de la misma fila, sino de la Vista de modelo de la tabla
             * por eso para obtener el Índice y el mensaje de espuesta se debe enlazar las acciones correspondientes 
             * al EventHandler de la actualización de la base de datos */
            pacienteTableViewModel.onUpdateTableEventHandler += delegate (object sender, OnUpdateTableEventArgs e) 
            {
                /* Pasar la respuesta desde la Vista de modelo de la tabla a la Vista de modelo de la fila*/
                pacienteViewModel.actionResponse.Set(e.actionResponse.responseText,e.actionResponse.responseType);

                /* Si actualización en la base de datos fue extitosa. Se procede a seleccionar la fila insertada */
                if (e.updateType == UpdateType.Added)
                {
                    pacienteTableSortKeyNin = pacienteViewModel.rowSelected == null ? string.Empty : pacienteViewModel.rowSelected["nin"].ToString();
                    PacienteSelectedRowSelectBySortKey(true);
                }
            };
            pacienteViewModel = new PacienteViewModel(applicationDataAccess);
            pacienteTableViewModel.CurrentRowViewModel = pacienteViewModel;

            // Registro
            registroTableViewModel = new RegistroTableViewModel(applicationDataAccess);
            registroTableViewModel.onUpdateTableEventHandler += delegate (object sender, OnUpdateTableEventArgs e)
            {
                /* Pasar la respuesta desde la Vista de modelo de la tabla a la Vista de modelo de la fila*/
                registroViewModel.actionResponse.Set(e.actionResponse.responseText, e.actionResponse.responseType);

                /* Si actualización en la base de datos fue extitosa. Se procede a seleccionar la fila insertada */

            };
            registroViewModel = new RegistroViewModel(applicationDataAccess);
            registroTableViewModel.CurrentRowViewModel = registroViewModel;

        }

    }
}


