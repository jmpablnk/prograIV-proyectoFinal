using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PIV_PF_ProyectoFinal.Controllers.ClientesController;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class DetallesFactura
    {
        public int IdDetallesFactura { get; set; }
        [Display(Name = "Subtotal")]
        [Required(ErrorMessage = "El campo Subtotal es requerido.")]
        public decimal Subtotal { get; set; }
        [Required(ErrorMessage = "El campo Total es requerido.")]
        public decimal Total { get; set; }
        [Required(ErrorMessage = "El campo Código Factura es requerido.")]
        public string CodigoFactura { get; set; } = null!;
        [Required(ErrorMessage = "El campo Código Producto es requerido.")]
        public string CodigoProducto { get; set; } = null!;
        [Display(Name = "Código Factura")]
        public virtual Factura CodigoFacturaNavigation { get; set; } = null!;
        [Display(Name = "Código Producto")]
        public virtual Producto CodigoProductoNavigation { get; set; } = null!;
    }
}
