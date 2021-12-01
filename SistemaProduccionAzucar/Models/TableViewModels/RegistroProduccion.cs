using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class RegistroProduccion
    {
        public int correlativo { get; set; }
        public int cod_materia { get; set; }
        public string materia_prima { get; set; }
        public decimal? cantidad { get; set; }
        public string unidad_m { get; set; }
        public string estado { get; set; }
        public int cod_producto { get; set; }
        public string producto { get; set; }
        public decimal? cantidad_producida { get; set; }
        public string unidad_p { get; set; }
        public DateTime? fecha_registro { get; set; }
        public string fechaRegistroStr { get { return fecha_registro?.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public DateTime? fecha_fin { get; set; }
        public string fechaFinStr { get { return fecha_fin?.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
    }
}