using System;
using CCP_HTA_2017.Gateway;
using System.Data;
using CCP_HTA_2017.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;
using CCP_HTA_2017.Bussines;

namespace CCP_HTA_2017.ViewModels
{
    public partial class ApplicationViewModel : ResponseViewModelBase
    {
        /// <summary> Vista de modelo de la tabla de pacientes en memoria</summary>
        public PacienteTableViewModel pacienteTableViewModel { get; set; } // (DataContext)

        /// <summary> Vista de modelo del paciente (<see cref="DataRowView"/>) actualmente seleccionado.</summary>
        public PacienteViewModel pacienteViewModel { set; get; }  // (DataContext)

        /// <summary> Parámetro foráneo de FindRowBySortKey(<see cref="DataView.Find(object)"/>). </summary>
        public string pacienteTableSortKeyNin // (Dato enlazable)
        {
            get { return _pacienteTableViewModelSortKeyNin; }
            set { SetProperty(ref _pacienteTableViewModelSortKeyNin, value); }
        }
        private string _pacienteTableViewModelSortKeyNin;

        //Methods:
        /// <summary> Agrega un nuevo paciente a la tabla en memoria y lo asgina como el paciente acualmete seleccionado. </summary>
        public void PacienteSelectedRowNewAdd()
        {
            PacienteSelectedRowChangeTo(pacienteTableViewModel.AddNew());
            pacienteViewModel.actionResponse.Set("Nuevo paciente.", Models.ResponseType.Success);
        }
        /// <summary> <see cref="ICommand"/> para <see cref="ApplicationViewModel.PacienteSelectedRowNewAdd"/>. </summary>
        public ICommand PacienteSelectedRowNewAddCommand
        {
            get
            {
                if (_PacienteSelectedRowNewAddCommand == null)
                {
                    _PacienteSelectedRowNewAddCommand = new RelayCommand(param => PacienteSelectedRowNewAdd(), param => true);
                }
                return _PacienteSelectedRowNewAddCommand;
            }
        }
        RelayCommand _PacienteSelectedRowNewAddCommand;

        /// <summary>Define el <see cref="DataRowView"/> recibido como el paciente actualmente seleccionado.</summary>
        /// <param name="row"><see cref="DataRowView"/>  de <see cref="PacienteTableViewModel"/> </param>  
        public DataRowView PacienteSelectedRowChangeTo ( DataRowView row )
        {
            return pacienteViewModel.rowSelected = row; // (!notify property change)
        }
        RelayCommand _PacienteSelectedRowChangeToCommand;
        public ICommand PacienteSelectedRowChangeToCommand
        {
            get
            {
                if (_PacienteSelectedRowChangeToCommand == null)
                {
                    _PacienteSelectedRowChangeToCommand = new RelayCommand( param => PacienteSelectedRowChangeTo(param as DataRowView),  param => !pacienteViewModel.IsEmpty );
                }
                return _PacienteSelectedRowChangeToCommand;
            }
        }

        /// <summary>Selecciona un paciente a partir de la <see cref="ApplicationViewModel.pacienteTableSortKeyNin"/>. </summary>
        /// <param name="newRow">Es verdadero si se selecciona un paciente luego de insertarlo en la base de datos. </param>  
        public void PacienteSelectedRowSelectBySortKey (bool newRow = false)
        {
            if (PacienteSelectedRowChangeTo(pacienteTableViewModel.FindRowBySortKey(pacienteTableSortKeyNin)) != null)
                pacienteViewModel.actionResponse.Set(newRow ? "Paciente '" + pacienteTableSortKeyNin + "' insertado en la base de datos." : "Paciente '" + pacienteTableSortKeyNin + "' encontrado.", Models.ResponseType.Success);
            else
                pacienteViewModel.actionResponse.Set("Paciente no encontrado.", Models.ResponseType.Warning);
        }
        /// <summary> <see cref="ICommand"/> para <see cref="ApplicationViewModel.PacienteSelectedRowSelectBySortKey"/>. </summary>
        public ICommand PacienteSelectedRowSelectBySortKeyCommand
        {
            get
            {
                if (_PacienteSelectedRowSelectBySortKeyCommand == null)
                {
                    _PacienteSelectedRowSelectBySortKeyCommand = new RelayCommand(param => PacienteSelectedRowSelectBySortKey(), param => !String.IsNullOrEmpty(pacienteTableSortKeyNin) );
                }
                return _PacienteSelectedRowSelectBySortKeyCommand;
            }
        }
        RelayCommand _PacienteSelectedRowSelectBySortKeyCommand;

        /// <summary> Deja nulo el paciente alctualmente seleccionado. </summary>
        public void PacienteSelecteRowEmpty()
        {
            pacienteViewModel.rowSelected = null;
            pacienteViewModel.actionResponse.Set("No se seleccionado o creado un paciente.", Models.ResponseType.Success);
        }
        /// <summary> <see cref="ICommand"/> para <see cref="ApplicationViewModel.PacienteSelecteRowEmpty"/>. </summary>
        public ICommand PacienteSelecteRowEmptyCommand
        {
            get
            {
                if (_PacienteSelecteRowEmptyCommand == null)
                {
                    _PacienteSelecteRowEmptyCommand = new RelayCommand(param => PacienteSelecteRowEmpty(), null);
                }
                return _PacienteSelecteRowEmptyCommand;
            }
        }
        RelayCommand _PacienteSelecteRowEmptyCommand;
    }
}


