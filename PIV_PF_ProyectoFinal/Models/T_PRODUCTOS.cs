//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIV_PF_ProyectoFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_PRODUCTOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_PRODUCTOS()
        {
            this.T_FACTURAS = new HashSet<T_FACTURAS>();
        }
    
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO { get; set; }
        public bool ESTADO { get; set; }
        public int CANTIDAD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_FACTURAS> T_FACTURAS { get; set; }
        public virtual T_TIPO_DE_PRODUCTOS T_TIPO_DE_PRODUCTOS { get; set; }
    }
}