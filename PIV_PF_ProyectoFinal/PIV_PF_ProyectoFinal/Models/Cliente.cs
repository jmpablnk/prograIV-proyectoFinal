using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PIV_PF_ProyectoFinal.Controllers.ClientesController;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdClientes { get; set; }
        [Required(ErrorMessage = "El campo Identificacion es requerido.")]
        [Display(Name = "Identificación")]
        [VALIDACIONCLIENTE(ErrorMessage = "La identificación ya está en uso.")]
        public string Identificacion { get; set; } = null!;
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [Display(Name = "Nombre Completo")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "El campo Nombre no debe contener números.")]
        public string NombreCliente { get; set; } = null!;
        [Required(ErrorMessage = "El campo Correo es requerido.")]
        [Display(Name = "Correo Electronico")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "El formato del correo debe ser gmail.com.")]
        public string Correo { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
