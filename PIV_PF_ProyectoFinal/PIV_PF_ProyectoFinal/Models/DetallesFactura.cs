using System;
using System.Collections.Generic;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class DetallesFactura
    {
        public int IdDetallesFactura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string CodigoFactura { get; set; } = null!;
        public string CodigoProducto { get; set; } = null!;

        public virtual Factura CodigoFacturaNavigation { get; set; } = null!;
        public virtual Producto CodigoProductoNavigation { get; set; } = null!;
    }
}
