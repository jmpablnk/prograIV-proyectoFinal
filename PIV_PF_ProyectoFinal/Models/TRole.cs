using System;
using System.Collections.Generic;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class TRole
    {
        public TRole()
        {
            TUsuarios = new HashSet<TUsuario>();
        }

        public int IdRole { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<TUsuario> TUsuarios { get; set; }
    }
}
