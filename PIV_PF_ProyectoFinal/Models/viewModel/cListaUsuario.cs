using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIV_PF_ProyectoFinal.Models.viewModel
{
    public class cListaUsuario
    {
        public int ID_Usuario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Apellidos_Usuario { get; set; }
        public string ESTADO { get; set; }
    }
}