using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP_HTA_2017.ViewModels;

namespace CCP_HTA_2017.Models
{
    public class SessionModel
    {
        public readonly DateTime start = DateTime.Now;
        public bool m_isLogged = false;
        public string userName;
    }
}
