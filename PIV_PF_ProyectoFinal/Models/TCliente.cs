using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TCliente
    {
        public TCliente()
        {
            TFacturas = new HashSet<TFactura>();
        }

        public int IdCliente { get; set; }
        public string Identificacion { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual ICollection<TFactura> TFacturas { get; set; }
    }
}
