//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaProduccionAzucar.Models.Fincas
{
    using System;
    using System.Collections.Generic;
    
    public partial class fincas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fincas()
        {
            this.registro_corte = new HashSet<registro_corte>();
            this.registro_sembrado = new HashSet<registro_sembrado>();
        }
    
        public int cod_finca { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Nullable<decimal> area_metros_cuadrados { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_corte> registro_corte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_sembrado> registro_sembrado { get; set; }
    }
}
