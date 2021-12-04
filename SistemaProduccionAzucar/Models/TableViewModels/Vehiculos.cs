using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class Vehiculos
    {
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public decimal capacidad { get; set; }
        public int? idDUI_motorista { get; set; }
        public string motorista { get; set; }
        public string estado_ingreso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string disponibilidad { get; set; }
        public int estado_registro { get; set; }
    }
}