//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaProduccionAzucar.Models.MateriaPrima
{
    using System;
    using System.Collections.Generic;
    
    public partial class fincas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fincas()
        {
            this.inventario_finca = new HashSet<inventario_finca>();
        }
    
        public int cod_finca { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Nullable<decimal> area_metros_cuadrados { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inventario_finca> inventario_finca { get; set; }
    }
}
