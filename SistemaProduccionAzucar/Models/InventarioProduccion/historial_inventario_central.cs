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
    
    public partial class historial_inventario_central
    {
        public int correlativo { get; set; }
        public int cod_materia_prima { get; set; }
        public decimal cantidad { get; set; }
        public int cod_cat { get; set; }
        public string comentario { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string usuario_creacion { get; set; }
    
        public virtual inventario_central inventario_central { get; set; }
    }
}
