using CCP_HTA_2017.Gateway;

namespace CCP_HTA_2017.ViewModels
{
    /*    Usuario = Row    */
    public class UsuarioViewModel : RowViewModelBase
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
        public UsuarioViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess) { }
    }
}
