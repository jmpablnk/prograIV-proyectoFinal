using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIV_PF_ProyectoFinal.Models.viewModel
{
    public class cPermiso
    {
        // Falta los tipos de persmisos //Esto para el acceso
    }

        public class PermisoRoleUsuario
        {
            public bool Eliminar { get; set; }
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
        }
        public class PermisoUsuario
        {
            public bool Eliminar { get; set; }
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
        }
        public class PermisoCategoriaProducto
        {
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
            public bool Eliminar { get; set; }
        }
        public class PermisoProducto
        {
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
            public bool Eliminar { get; set; }
        }
        public class PermisoCliente
        {
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
            public bool Eliminar { get; set; }
        }
        public class PermisoDetalleFactura
        {
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
            public bool Eliminar { get; set; }
        }
        public class PermisoFactura
        {
            public bool Guardar { get; set; }
            public bool Editar { get; set; }
            public bool Acceso { get; set; }
            public bool Eliminar { get; set; }
        }
    
}