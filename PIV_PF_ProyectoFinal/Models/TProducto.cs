using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TProducto
    {
        public TProducto()
        {
            TFacturas = new HashSet<TFactura>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool? Estado { get; set; }
        public int Cantidad { get; set; }
        public int IdTipoProducto { get; set; }

        public virtual TTipoDeProducto IdTipoProductoNavigation { get; set; } = null!;
        public virtual ICollection<TFactura> TFacturas { get; set; }
    }
}
