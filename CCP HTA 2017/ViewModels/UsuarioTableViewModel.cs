using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP_HTA_2017.Gateway;
using CCP_HTA_2017.Bussines;
using CCP_HTA_2017.Models;

namespace CCP_HTA_2017.ViewModels
{
    public class UsuarioTableViewModel : TableViewModelBase
    {
        /// <summary> .</summary>
        private string leerContraseñaAlmacenada(string nombreUsuario)
        {
            return applicationDataAccess.usuarioTableAdapter.GetPassword(nombreUsuario).ToString();
        }

        /// <summary> .</summary>
        public bool compararContraseña(string contraseñaIngresada, string nombreUsuario)
        {
            bool coincidencia = false;
            string contraseñaEncriptada = Encryptor.Encrypt(contraseñaIngresada);
            if (contraseñaEncriptada == leerContraseñaAlmacenada(nombreUsuario)) coincidencia = true;
            return coincidencia;
        }

        /// <summary> .</summary>
        public bool actualizarContraseña(string contraseñaNueva, string nombreUsuario)
        {
            string contraseñaEncriptada = Encryptor.Encrypt(contraseñaNueva);
            int exito = applicationDataAccess.usuarioTableAdapter.SetContraseña(contraseñaEncriptada, nombreUsuario);
            return exito != 0;
        }

        /// <summary> . </summary>
        override public void UpdateTable(bool LookCurrentRowViewModel = false)
        {
            UpdateTableAdapter(TableAdapter.usuario, LookCurrentRowViewModel);
        }

        override public DataRowView FindRowBySortKey(object sortKey)
        {
            var usuarioSortKey = sortKey as string;
            int rowIndex = tableViewModel.Find(usuarioSortKey);     // DataView.Find (Object): https://msdn.microsoft.com/es-es/library/46d41xk2(v=vs.110).aspx 
                                                                    // The index of the row in the System.Data.DataView that contains the sort key value
                                                                    // specified; otherwise -1 if the sort key value does not exist.

            if (rowIndex == -1)
            {
                actionResponse.Set("No se econtró el usuario.", ResponseType.Error);
                throw new DataException();
            }
            else
            {
                actionResponse.Set("Se enctontró el usuario.", ResponseType.Success);
            }

            return tableViewModel[rowIndex];
        }

        //Contructor
        /// <summary> .Inherit contructor of  <see cref="TableViewModelBase"/> </summary>
        public UsuarioTableViewModel(ApplicationDataAccess applicationDataAccess) : base(applicationDataAccess)
        {
            this.tableViewModel = applicationDataAccess.dataSet.usuario.DefaultView;
            tableViewModel.Sort = "idusuario ASC";
        }
    }
}
