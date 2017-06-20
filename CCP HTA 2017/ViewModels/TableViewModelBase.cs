using CCP_HTA_2017.Commands;
using CCP_HTA_2017.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using CCP_HTA_2017.Models;
using static CCP_HTA_2017.ViewModels.RegistroTableViewModel;
using System.Data.SQLite;

namespace CCP_HTA_2017.ViewModels
{
    public abstract class TableViewModelBase : ResponseViewModelBase
    {
        /// <summary> . </summary>
        public ApplicationDataAccess applicationDataAccess;

        /// <summary> Representa la <see cref="DataView"/> de la tabla de pacientes en memoria </summary>
        public DataView tableViewModel // (Binding data) change?
        {
            get { return m_tableViewModel; }
            set
            {
                SetProperty(ref m_tableViewModel, value);
                FillUniqueConstraintsColumnOrdinal();
            }
        }
        private DataView m_tableViewModel = null;

        /// <summary> </summary>
        public RowViewModelBase CurrentRowViewModel;
        
        /// <summary> </summary>
        public bool IsNull
        {
            get { return tableViewModel == null; }
        }

        /// <summary>NewRow.Row.RowState == DataRowState.Detached || NewRow.Row.RowState == DataRowState.Added</summary>
        public bool HasNewRow
        {
            get
            {
                if (CurrentRowViewModel == null || CurrentRowViewModel.rowSelected == null)
                    return false;
                else
                    return CurrentRowViewModel.rowSelected.Row.RowState == DataRowState.Detached || CurrentRowViewModel.rowSelected.Row.RowState == DataRowState.Added;
            }
        }    

        /// <summary> . </summary>
        public enum TableAdapter { paciente, registro, usuario };
        
        /// <summary>Índice de la fila nueva (si la hay).</summary>
        public delegate void OnUpdateTableEventHandler(object sender, OnUpdateTableEventArgs e);
        /// <summary>Índice de la fila nueva (si la hay).</summary>
        public event OnUpdateTableEventHandler onUpdateTableEventHandler;
        /// <summary>Índice de la fila nueva (si la hay).</summary>
        protected virtual void OnUpdateTable(OnUpdateTableEventArgs e)
        {
            onUpdateTableEventHandler?.Invoke(this, e);
        }


        //Methods
        public DataRowView AddNew ()
        {
            if (CurrentRowViewModel.rowSelected!=null)
                CurrentRowViewModel.rowSelected.CancelEdit();
            return tableViewModel.AddNew();
        }

        /// <summary> . </summary>
        public abstract DataRowView FindRowBySortKey(object sortkey);


        List<int> uniqueConstraintsColumnOrdinals = new List<int>();
        protected void FillUniqueConstraintsColumnOrdinal()
        {

            foreach (Constraint constraint in tableViewModel.Table.Constraints)
            {
                if (constraint is UniqueConstraint)
                {
                    UniqueConstraint uniqueConstraint;
                    uniqueConstraint = (UniqueConstraint)constraint;
                    foreach (DataColumn column in uniqueConstraint.Columns)
                    {
                        uniqueConstraintsColumnOrdinals.Add(column.Ordinal);
                    }
                }
            }
        }

        public DataRowView RowSelectedDuplicate ()
        {
            if (CurrentRowViewModel.rowSelected == null || CurrentRowViewModel.rowSelected.IsNew) return null;
            DataRowView copy = tableViewModel.AddNew();
            int i = 0;
            foreach (object obj in copy.Row.ItemArray)
            {
                string columnName = CurrentRowViewModel.rowSelected.Row.Table.Columns[i].ColumnName;
                if (!uniqueConstraintsColumnOrdinals.Contains(i))
                {
                    copy[i] = CurrentRowViewModel.rowSelected[i];
                }
                i++;
            }
            OnPropertyChanged("tableViewModel");
            return CurrentRowViewModel.rowSelected = copy;
        }

        public ICommand RowSelectedDuplicateCommand
        {
            get
            {
                if (_RowSelectedDuplicateCommand == null)
                {
                    _RowSelectedDuplicateCommand = new RelayCommand(param => RowSelectedDuplicate(), param => CurrentRowViewModel.rowSelected != null && !CurrentRowViewModel.rowSelected.IsNew);
                }
                return _RowSelectedDuplicateCommand;
            }
        }
        RelayCommand _RowSelectedDuplicateCommand;

        /// <summary> Compara y realzia los cambios de la tabla en memoria con la tabla de la base de datos. </summary>
        public void UpdateTableAdapter(TableAdapter tableAdapter, bool LookCurrentRowViewModel = false)
        {
            OnUpdateTableEventArgs onUpdateTableEventArgs = new OnUpdateTableEventArgs(new ActionResponseViewModel());
            int SuccessfullyUpdatedRows = 0;
            string tableName = "ningúna";
            switch (tableAdapter)
            {
                case (TableAdapter.paciente):   tableName = "paciente"; break;
                case (TableAdapter.registro):   tableName = "registro"; break;
                case (TableAdapter.usuario):    tableName = "usuario"; break;
            }
            string message;
            string messageFormat;
            bool rowExists = false;

            /* Si se obtiene ConstraintException se ha violado las restriccioens de
             * llave primaria de la tabla estrucura de la tabla */
            if (HasNewRow) // NewRow is Detached or Added
            {
                try
                {
                    CurrentRowViewModel.rowSelected.EndEdit();
                }
                catch (ConstraintException cEx)
                {
                    messageFormat = "Ya existe una fila en la tabla '{0}'";
                    rowExists = true;
                }
            }
//            tableViewModel.Table.AcceptChanges();

            /* Una vez se han realizados los cambios en la tabla en memoria
             * se almacena el tipo de actualización según el estado de la fila
             * si es que el cambio no afetca a varias filas a la vez */
            if (LookCurrentRowViewModel)
            {
                switch (CurrentRowViewModel.rowSelected.Row.RowState)
                {
                    case DataRowState.Added:
                        onUpdateTableEventArgs.updateType = UpdateType.Added;
                        messageFormat = "Se agregó el {0} en la tabla '{0}' en la base de datos.";
                        break;
                    case DataRowState.Deleted:
                        onUpdateTableEventArgs.updateType = UpdateType.Deleted;
                        messageFormat = "Se eliminó el {0} en la tabla '{0}' en la base de datos.";
                        break;
                    case DataRowState.Detached:
                        onUpdateTableEventArgs.updateType = UpdateType.Detached;
                        messageFormat = "Se removió el {0} de la tabla '{0}' en la base de datos.";
                        break;
                    case DataRowState.Modified:
                        onUpdateTableEventArgs.updateType = UpdateType.Modified;
                        messageFormat = "Se guardaron los cambios del {0} en la tabla '{0}' en la base de datos.";
                        break;
                    case DataRowState.Unchanged:
                        onUpdateTableEventArgs.updateType = UpdateType.Unchanged;
                        messageFormat = "No hubo cambios en la tabla '{0}' en la base de datos.";
                        break;
                    default:
                        onUpdateTableEventArgs.updateType = UpdateType.Undefined;
                        messageFormat = "No se definieron los cambios en la tabla '{0}' en la base de datos.";
                        break;
                }
            }
            else
            {
                messageFormat = "Se guardaron los cambios en la tabla '{0}' en la base de datos.";
                onUpdateTableEventArgs.updateType = UpdateType.MultiRow;
            }

            /* El TableAdapter es diferente para cada tabla en memoria.
             A continuacón se efectuan los cambios en la base de datos*/
            if (!rowExists)
            {
                try
                {
                    switch (tableAdapter)
                    {
                        case (TableAdapter.paciente):
                            SuccessfullyUpdatedRows = applicationDataAccess.pacienteTableAdapter.Update(applicationDataAccess.dataSet.paciente);
                            break;

                        case (TableAdapter.registro):
                            SuccessfullyUpdatedRows = applicationDataAccess.registroTableAdapter.Update(applicationDataAccess.dataSet.registro);
                            break;

                        case (TableAdapter.usuario):
                            tableName = "usuario";
                            SuccessfullyUpdatedRows = applicationDataAccess.usuarioTableAdapter.Update(applicationDataAccess.dataSet.usuario);
                            break;

                        default:
                            SuccessfullyUpdatedRows = 0;
                            break;
                    }
                }
                catch (SQLiteException sqlEx)
                {
                    switch (sqlEx.ErrorCode)
                    {
                        case (SQLiteErrorCode.Constraint):
                            messageFormat = "El {0} ya existe en la talba '{0}' en la base de datos.";
                            break;

                        default:
                            messageFormat = "No se logró efectuar lso cambios en la talba '{0}' en la base de datos. Error desconodido";
                            break;
                    }
                }
            }

            /* Preparar el mensaje */
            message = String.Format(messageFormat, tableName);
           
            if (SuccessfullyUpdatedRows != 0)            
                actionResponse.Set(message, ResponseType.Success);                            
            else            
                actionResponse.Set(message, ResponseType.Error);
            
            /*   */
            onUpdateTableEventArgs.actionResponse = actionResponse;
            OnUpdateTable(onUpdateTableEventArgs);
        }
        /// <summary> . </summary>
        public abstract void UpdateTable(bool LookCurrentRowViewModel = false);

        /// <summary>Comando para actualizar la tabla de la base de datos.</summary>
        public ICommand UpdateTableCommand
        {
            get
            {
                if (_UpdateTableCommand == null)
                {
                    _UpdateTableCommand = new RelayCommand(param => UpdateTable(), param => !CurrentRowViewModel.IsEmpty);
                }
                return _UpdateTableCommand;
            }
        }
        RelayCommand _UpdateTableCommand;

        /// <summary>Comando para actualizar la tabla de la base de datos y operar sobre la fila actualmente seleccionada.</summary>
        public ICommand UpdateTableLookCurrentRowCommand
        {
            get
            {
                if (_UpdateTableLookCurrentRowCommand == null)
                {
                    _UpdateTableLookCurrentRowCommand = new RelayCommand(param => UpdateTable(true), param => !IsNull && CurrentRowViewModel.rowSelected != null); 
                }
                return _UpdateTableLookCurrentRowCommand;
            }
        }
        RelayCommand _UpdateTableLookCurrentRowCommand;

        /// <summary> </summary>   
        public void DeleteCurrentRow()
        {
            CurrentRowViewModel.rowSelected.Delete();            
            UpdateTable(true);
            CurrentRowViewModel.rowSelected = null;
        }

        /// <summary> </summary>    
        public ICommand DeleteCurrentRowCommand
        {
            get
            {
                if (_DeleteCurrentRowCommand == null)
                {
                    _DeleteCurrentRowCommand = new RelayCommand(param => DeleteCurrentRow(), param => !CurrentRowViewModel.IsEmpty);
                }
                return _DeleteCurrentRowCommand;
            }
        }
        RelayCommand _DeleteCurrentRowCommand;

        //Contructor:
        /// <summary> . </summary>
        public TableViewModelBase(ApplicationDataAccess applicationDataAccess)
        {
            this.applicationDataAccess = applicationDataAccess;            
        }
    }
    public enum UpdateType { Added, Deleted, Detached, Modified, Unchanged, MultiRow, Undefined };
    public class OnUpdateTableEventArgs : EventArgs
    {
        public UpdateType updateType;
        public ActionResponseViewModel actionResponse;
        public OnUpdateTableEventArgs(ActionResponseViewModel actionResponse, UpdateType UpdateType = UpdateType.Modified)
        {
            this.updateType = UpdateType;
            this.actionResponse = actionResponse;
        }
    }
}
