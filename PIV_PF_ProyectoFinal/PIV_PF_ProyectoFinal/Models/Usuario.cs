using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PIV_PF_ProyectoFinal.Controllers.UsuariosController;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo Identificacion es requerido.")]
        [Display(Name = "Identifiacion")]
        [VALIDACION_USUARIO(ErrorMessage = "La identificación ya está en uso.")]
        public string IdentificacionUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [Display(Name = "Nombre Completo")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "El campo Nombre no debe contener números.")]
        public string NombreCompletoUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El campo Correo es requerido.")]
        [Display(Name = "Correo Electronico")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "El formato del correo debe ser gmail.com.")]
        public string CorreoUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El campo Estado es requerido. (Administrador, Vendedor, Contador)")]
        [EnumDataType(typeof(TipoUsuarioEnum))]
        [Display(Name = "Estado: Administrador,Vendedor o Contador")]
        public string TipoUsuario { get; set; } = null!;
        public enum TipoUsuarioEnum
        {
            Administrador,
            Vendedor,
            Contador
        }
        [Required(ErrorMessage = "El campo Estado es requerido.")]
        [RegularExpression("^(Activo|Inactivo)$", ErrorMessage = "El campo Estado debe ser 'Activo' o 'Inactivo'.")]
        [Display(Name = "Estado: Activo o Inactivo")]
        public string EstadoUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El campo Clave es requerido.")]
        [Display(Name = "Clave")]
        public string ContrasenaUsuario { get; set; } = null!;
    }
}

