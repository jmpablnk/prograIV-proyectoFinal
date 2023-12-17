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
        public string CodigoFactura { get; set; } = null!;
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
        [Display(Name = "Método de Pago")]
        public string MetodoPago { get; set; } = null!;
        [Display(Name = "ID Cliente")]
        public int IdClientes { get; set; }
        [Display(Name = "ID Cliente")]
        public virtual Cliente IdClientesNavigation { get; set; } = null!;
        public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; }
    }
}
