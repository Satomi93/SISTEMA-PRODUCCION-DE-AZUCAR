//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaProduccionAzucar.Models.InventarioProduccion
{
    using System;
    using System.Collections.Generic;
    
    public partial class inventario_produccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inventario_produccion()
        {
            this.registro_produccion = new HashSet<registro_produccion>();
        }
    
        public int cod_producto { get; set; }
        public string descripcion { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public string unidad { get; set; }
        public string tipo_producto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_produccion> registro_produccion { get; set; }
    }
}