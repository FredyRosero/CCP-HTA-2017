using CCP_HTA_2017.Bussines;
using CCP_HTA_2017.Commands;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace CCP_HTA_2017.ViewModels
{
    /*    Paciente = Row    */
    public class PacienteViewModel : RowViewModelBase
    {
        //ViewModel State:

        /// <summary>TFG calculado con el último registor del paciente. </summary>
        public double? tfg
        {
            get
            {
                if (IsEmpty) return null;
                if (childRegistroTableViewModel==null || childRegistroTableViewModel.Count==0) return null;
                double? tfg = null;
                DataRowView utlimoRegistro = this.childRegistroTableViewModel[0];
                if (utlimoRegistro != null)
                {
                    int edad = DateTime.Now.Year - ((DateTime)rowSelected["nacimiento"]).Year;
                    short peso = (short)utlimoRegistro["peso"];
                    decimal creatinina = (decimal)utlimoRegistro["creatinina"];
                    tfg = (double)( ( (140 - edad) * peso) / (creatinina * 72) );
                }
                return tfg;
            }
        }

        /// <summary> Tabla de Registros hija de Paciente </summary>
        public DataView childRegistroTableViewModel { set; get; } = null; // (DataContext)

        /// <summary> </summary>
        override public void OnSetRowSelected()
        {
            if (rowSelected == null || rowSelected.Row.RowState == DataRowState.Detached)
                childRegistroTableViewModel = null;
            else
                childRegistroTableViewModel = rowSelected.CreateChildView("FK_registro_0_0");
            OnPropertyChanged("childRegistroTableViewModel");
            OnPropertyChanged("IsEmpty");
            OnPropertyChanged("IsNew");            
            OnPropertyChanged("IsEdit");
            OnPropertyChanged("tfg");
            
        }


        //Contructor:
        /// <summary> . </summary>
        public PacienteViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess) { }
    }
}
