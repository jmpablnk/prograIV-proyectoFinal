using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class TiposProducto
    {
        public TiposProducto()
        {
            Productos = new HashSet<Producto>();
        }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "El código debe ser alfanumérico.")]
        public string CodigoTipoProducto { get; set; } = null!;
        [Required(ErrorMessage = "El campo Descripcion es requerido.")]
        public string DescripcionTipoProducto { get; set; } = null!;


        public virtual ICollection<Producto> Productos { get; set; }
    }
}
