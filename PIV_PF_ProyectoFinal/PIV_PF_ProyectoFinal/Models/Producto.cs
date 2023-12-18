using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIV_PF_ProyectoFinal.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallesFacturas = new HashSet<DetallesFactura>();
        }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "El código debe ser alfanumérico.")]
        public string CodigoProducto { get; set; } = null!;
        [Required(ErrorMessage = "El campo Descripcion es requerido.")]

        public string DescripcionProducto { get; set; } = null!;
        [Required(ErrorMessage = "El campo precio es requerido.")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "El campo Estado es requerido.")]
        [RegularExpression("^(Agotado|En existencia)$", ErrorMessage = "El campo Estado debe ser 'Agotado' o 'En existencia'.")]
        [Display(Name = "Estado: Agotado o En existencia")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "La cantidad en stock es requerida.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "La cantidad en stock debe ser un número.")]
        public int CantidadStock { get; set; }
        [Required(ErrorMessage = "El codigo tiene que ser Valido.")]
        public string? CodigoTipoProducto { get; set; }

        public virtual TiposProducto? CodigoTipoProductoNavigation { get; set; }
        public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; }
    }
}
