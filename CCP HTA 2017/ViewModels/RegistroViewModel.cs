using CCP_HTA_2017.Commands;
using CCP_HTA_2017.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CCP_HTA_2017.ViewModels
{
    /*    Registro = Row    */
    public class RegistroViewModel : RowViewModelBase
    {

        //Data Controller
        /// <summary> . </summary>
        public override void OnSetRowSelected()
        {
            OnPropertyChanged("IsEmpty");
            OnPropertyChanged("IsNew");
            OnPropertyChanged("IsEdit");
        }


        //Contructor:
        /// <summary> . </summary>
        public RegistroViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess) { }
    }
}
