using System;
using CCP_HTA_2017.Gateway;
using System.Data;
using CCP_HTA_2017.Commands;
using System.Windows.Input;
using System.ComponentModel;

namespace CCP_HTA_2017.ViewModels
{
    public partial class ApplicationViewModel : ResponseViewModelBase
    {
        //ViewModels:       

        /// <summary> Tabla de registros </summary>
        public RegistroTableViewModel registroTableViewModel { set; get; }  // (DataContext)

        /// <summary> Tabla de Registros hija de Paciente </summary>
        public RegistroViewModel registroViewModel { set; get; }  // (DataContext)


        //Properties:

        /// <summary> . </summary>
        public DateTime registroTableViewModelSortKeyFecha // (Dato enlazable)
        {
            get { return _registroTableViewModelSortKeyFecha; }
            set { SetProperty(ref _registroTableViewModelSortKeyFecha, value); }
        }
        DateTime _registroTableViewModelSortKeyFecha;

        /// <summary> . </summary>
        public RegistroTableViewModel.RegistroSortKey registroTableViewModelSortKeyNinAndFecha // (Dato enlazable)
        {
            get
            {
                return new RegistroTableViewModel.RegistroSortKey(
                    pacienteTableSortKeyNin,
                    registroTableViewModelSortKeyFecha
                );
            }
        }


        //Controller:

        /// <summary> . </summary>
        public void RegistroSelectedRowNewAdd()
        {
            RegistroSelectedRowChangeTo( registroTableViewModel.tableViewModel.AddNew() );
            pacienteViewModel.actionResponse.Set("Nuevo registro.", Models.ResponseType.Success);
        }
        RelayCommand _RegistroSelectedRowNewAddRowCommand;
        public ICommand RegistroSelectedRowNewAddRowCommand
        {
            get
            {
                if (_RegistroSelectedRowNewAddRowCommand == null)
                {
                    _RegistroSelectedRowNewAddRowCommand = new RelayCommand(param => RegistroSelectedRowNewAdd(), param => true);
                }
                return _RegistroSelectedRowNewAddRowCommand;
            }
        }


        /// <summary> . </summary>
        public void RegistroSelectedRowChangeTo(DataRowView row)
        {
            registroViewModel.rowSelected = row; //(!notify property change)
        }
        RelayCommand _RegistroSelectedRowChangeToCommand;
        public ICommand RegistroSelectedRowChangeToCommand
        {
            get
            {
                if (_RegistroSelectedRowChangeToCommand == null)
                {
                    _RegistroSelectedRowChangeToCommand = new RelayCommand(param => RegistroSelectedRowChangeTo(param as DataRowView), param => !registroViewModel.IsEmpty);
                }
                return _RegistroSelectedRowChangeToCommand;
            }
        }

        /// <summary> . </summary>
        public void RegistroSelectedRowSelectBySortKey()
        {
            RegistroSelectedRowChangeTo( registroTableViewModel.FindRowBySortKey(registroTableViewModelSortKeyNinAndFecha) );
        }
        RelayCommand _RegistroSelectedRowSelectBySortKeyCommand;
        public ICommand RegistroSelectedRowSelectBySortKeyCommand
        {
            get
            {
                if (_RegistroSelectedRowSelectBySortKeyCommand == null)
                {
                    _RegistroSelectedRowSelectBySortKeyCommand = new RelayCommand(param => RegistroSelectedRowSelectBySortKey(), param => !registroViewModel.IsEmpty);
                }
                return _RegistroSelectedRowSelectBySortKeyCommand;
            }
        }

    }
}


