using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PIV_PF_ProyectoFinal.Models.viewModel
{
    public class RolesUsuarios
    {
        public int IDRole { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        public cPermiso Permisos { get; set; } = new cPermiso();
    }

}