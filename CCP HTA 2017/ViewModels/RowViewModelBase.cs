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

namespace CCP_HTA_2017.ViewModels
{
    public class OnSetRowSelectedEventArgs : EventArgs
    {
        public DataRowView dataRowView;
    }
    public abstract class RowViewModelBase : ResponseViewModelBase
    {
        //Data Access
        /// <summary> . </summary>
        public ApplicationDataAccess applicationDataAccess;


        //Fileds

        /// <summary> Representa el <see cref="DataRowView"/> de la fila actualmetne seleccionada. (Dato enlazable)  </summary>
        public DataRowView rowSelected
        {
            get { return m_selectedRow; }
            set
            {
                if (m_selectedRow != null)
                {
                    m_selectedRow.CancelEdit();
                }
                SetProperty(ref m_selectedRow, value);
                OnSetRowSelected();
                OnSetRowSelectedEventHandler?.Invoke(this, new OnSetRowSelectedEventArgs() { dataRowView = value });
            }
        }
        private DataRowView m_selectedRow;

        /// <summary> </summary>
        public abstract void OnSetRowSelected();
        public delegate void OnSetRowSelectedEventHandlerDelegate (object sender, OnSetRowSelectedEventArgs e);
        public event OnSetRowSelectedEventHandlerDelegate OnSetRowSelectedEventHandler;


        /// <summary> </summary>
        public bool IsEmpty
        {
            get { return rowSelected == null; }
        }

        /// <summary> </summary>
        public bool IsNew
        {
            get { return IsEmpty? false : rowSelected.IsNew; }
        }

        /// <summary> </summary>
        public bool IsEdit
        {
            get { return IsEmpty ? false : rowSelected.IsEdit; }
        }

        //Methos And Commands

        /// <summary> </summary>    
        public ICommand BeginEditCommand
        {
            get
            {
                if (_BeginEditCommand == null)
                {
                    _BeginEditCommand = new RelayCommand(param => rowSelected.BeginEdit(), param => !IsEmpty);
                }
                return _BeginEditCommand;
            }
        }
        RelayCommand _BeginEditCommand;

        /// <summary> </summary>        
        public ICommand CancelEditCommand
        {
            get
            {
                if (_CancelEditCommand == null)
                {
                    _CancelEditCommand = new RelayCommand(param => rowSelected.CancelEdit(), param => !IsEmpty);
                }
                return _CancelEditCommand;
            }
        }
        RelayCommand _CancelEditCommand;

        /// <summary> </summary>        
        public void EndEdit()
        {
            try
            {
                rowSelected.EndEdit();
            }
            catch (NoNullAllowedException NoNullEx)
            {
                actionResponse.Set(NoNullEx.Message,Models.ResponseType.Error);
                //selectedRow.CancelEdit(); <- rollback
                rowSelected.BeginEdit();
            }            
        }
        public ICommand EndEditCommand
        {
            get
            {
                if (_EndEditCommand == null)
                {
                    _EndEditCommand = new RelayCommand(param => EndEdit(), param => !IsEmpty);
                }
                return _EndEditCommand;
            }
        }
        RelayCommand _EndEditCommand;

        public override string ToString()
        {
            if (rowSelected == null) return "Null RowViewModel!";            
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int i = 0;
            foreach (object obj in rowSelected.Row.ItemArray)
            {
                sb.Append(rowSelected.Row.Table.Columns[i].ColumnName + "=" + obj + ": (" + obj.GetType() + "), ");
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }

        //Contructor:
        /// <summary> . </summary>
        public RowViewModelBase(ApplicationDataAccess applicationDataAccess)
        {
            this.applicationDataAccess = applicationDataAccess;
        }
    }
}
