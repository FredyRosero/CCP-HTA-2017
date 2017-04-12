using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP_HTA_2017.Models
{
    public enum ResponseType { Success, Warning, Error };
    public class ActionResponse 
    {
        public ResponseType responseType;
        public string responseText;
        public ActionResponse()
        {

        }
        public ActionResponse(string responseText, ResponseType responseType = ResponseType.Error)
        {
            this.responseType = responseType;
            this.responseText = responseText;
        }
    }
}
