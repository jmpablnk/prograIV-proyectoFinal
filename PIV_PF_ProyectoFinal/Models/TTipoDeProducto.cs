using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TTipoDeProducto
    {
        public TTipoDeProducto()
        {
            TProductos = new HashSet<TProducto>();
        }

        public int IdTipoProducto { get; set; }
        public string NombreTipoProducto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<TProducto> TProductos { get; set; }
    }
}
