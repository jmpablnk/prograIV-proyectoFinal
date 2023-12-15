using System;
using System.Collections.Generic;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetallesFacturas = new HashSet<DetallesFactura>();
        }
        //hay que modificarlo 

        public string CodigoFactura { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
        public int Cantidad { get; set; }
        public string MetodoPago { get; set; } = null!;
        public int IdClientes { get; set; }

        public virtual Cliente IdClientesNavigation { get; set; } = null!;
        public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; }
    }
}
