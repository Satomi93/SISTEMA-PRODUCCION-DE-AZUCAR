using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class HistorialInventarioProduccion
    {
        public int correlativo { get; set; }
        public int cod_materia_prima { get; set; }
        public decimal cantidad { get; set; }
        public int cod_cat { get; set; }
        public string comentario { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string usuario_creacion { get; set; }
    }
}