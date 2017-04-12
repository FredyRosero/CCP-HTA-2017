using CCP_HTA_2017.Commands;

namespace CCP_HTA_2017.ViewModels
{
    public abstract class ResponseViewModelBase : ViewModelBase
    {
        /// <summary>Respuesta de las métodos para enlazar.</summary>
        public ActionResponseViewModel actionResponse { get; set; } = new ActionResponseViewModel();
    }
}
