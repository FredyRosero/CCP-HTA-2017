using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP_HTA_2017.Models;

namespace CCP_HTA_2017.ViewModels
{
    public class ActionResponseViewModel : ViewModelBase
    {
        private ActionResponse actionResponse = new ActionResponse();
        public string responseText
        {
            get { return actionResponse.responseText; }
            set { SetProperty(ref actionResponse.responseText, value); }
        }
        public ResponseType responseType
        {
            get { return actionResponse.responseType; }
            set { SetProperty(ref actionResponse.responseType, value); }
        }
        public void Set(string responseText, ResponseType responseType)
        {
            this.responseText = responseText;
            this.responseType = responseType;
        }
    }
}
