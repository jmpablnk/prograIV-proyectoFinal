using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TFactura
    {
        public int IdFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal MontoTotal { get; set; }
        public bool? Estadoactivo { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }

        public virtual TCliente IdClienteNavigation { get; set; } = null!;
        public virtual TProducto IdProductoNavigation { get; set; } = null!;
        public virtual TUsuario IdUsuarioNavigation { get; set; } = null!;
    }
}
