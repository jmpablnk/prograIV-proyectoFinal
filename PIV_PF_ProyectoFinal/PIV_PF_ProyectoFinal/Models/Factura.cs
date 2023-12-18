using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetallesFacturas = new HashSet<DetallesFactura>();
        }
        //hay que modificarlo 
        [Display(Name = "Código Factura")]
        [Required(ErrorMessage = "El campo Codigo es requerido.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "El código debe ser alfanumérico.")]
        public string CodigoFactura { get; set; } = null!;
        [Display(Name = "Fecha de Compra")]
        [Required(ErrorMessage = "El campo Fecha es requerido.")]
        public DateTime FechaCompra { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo Cantidad es requerido.")]
        public int Cantidad { get; set; }
        [Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "El Metodo de pago es requerido.")]
        public string MetodoPago { get; set; } = null!;
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo cliente es requerido.")]
        public int IdClientes { get; set; }
        public virtual Cliente IdClientesNavigation { get; set; } = null!;
        public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; }
    }
}
