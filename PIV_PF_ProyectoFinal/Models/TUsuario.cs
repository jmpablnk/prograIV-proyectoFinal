using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TUsuario
    {
        public TUsuario()
        {
            TFacturas = new HashSet<TFactura>();
        }

        public int IdUsuario { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int IdRole { get; set; }

        public virtual TRole IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<TFactura> TFacturas { get; set; }
    }
}
