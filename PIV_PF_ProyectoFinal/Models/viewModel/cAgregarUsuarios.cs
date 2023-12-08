using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIV_PF_ProyectoFinal.Models.viewModel
{
    public class cAgregarUsuarios
    {
            [Required]
            [Display(Name = "Identificacion")]
            public string IDENTIFICACION { get; set; }
            [Required]
            [Display(Name = "Nombre")]
            public string NOMBRE { get; set; }
            [Required]
            [Display(Name = "Apellidos")]
            public string APELLIDOS { get; set; }
            [Required]
            [Display(Name = "Correo")]
            public string CORREO { get; set; }
            [Required]
            [Display(Name = "Clave")]
            public string CLAVE { get; set; }
            [Required]
            [Display(Name = "Estado")]
            public string ESTADO { get; set; }
            [Required]
            [Display(Name = "Role de usuario")]
            public int IDROLE { get; set; }
        
    }
}