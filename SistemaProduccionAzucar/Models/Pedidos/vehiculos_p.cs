//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaProduccionAzucar.Models.Pedidos
{
    using System;
    using System.Collections.Generic;
    
    public partial class vehiculos_p
    {
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public decimal capacidad { get; set; }
        public Nullable<int> idDUI_motorista { get; set; }
        public string motorista { get; set; }
        public string estado_ingreso { get; set; }
        public System.DateTime fecha_registro { get; set; }
        public string disponibilidad { get; set; }
        public int estado_registro { get; set; }
    
        public virtual motoristas_p motoristas { get; set; }
    }
}