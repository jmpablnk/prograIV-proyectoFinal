using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIV_PF_ProyectoFinal.Models.viewModel
{
    public class cRoles
    {
        public int ID_Roles { get; set; }
        public string NombreRoles { get; set; }
        public cPermiso permisos { get; set; } = new cPermiso();
    }
}