using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCP_HTA_2017.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.Bussines;
using System.Diagnostics;

namespace CCP_HTA_2017.ViewModels.Tests
{
    [TestClass()]
    public class PacienteTableVm_Tests : CCP_HTA_2017Tests.UnitTestBase
    {
        //Pre
        public PacienteTableViewModel pacienteTableViewModel;
        public ApplicationDataAccess applicationDataAccess = new ApplicationDataAccess();
        public PacienteTableVm_Tests()
        {
            applicationDataAccess.pacienteTableAdapter.Fill(applicationDataAccess.dataSet.paciente);
            applicationDataAccess.registroTableAdapter.Fill(applicationDataAccess.dataSet.registro);
            pacienteTableViewModel = new PacienteTableViewModel(applicationDataAccess);
        }        


        // Arrange
        string ninExistente = "1";
        string ninNoExistente = "-1";
        string nin11Carácteres = "12345678901";
            

        [TestMethod()]
        public void PacienteTableVm_FindPaciente()
        {
            PacienteViewModel pacienteViewModel = new PacienteViewModel(applicationDataAccess);
            pacienteViewModel.rowSelected = pacienteTableViewModel.FindRowBySortKey(ninExistente);
            Trace.WriteLine("DataRowView paciente = pacienteTableViewModel.FindPaciente(ninExistente) = " + pacienteViewModel);
        }

    }
}