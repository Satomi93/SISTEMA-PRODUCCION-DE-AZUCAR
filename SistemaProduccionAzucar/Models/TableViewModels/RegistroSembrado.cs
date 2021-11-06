using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class RegistroSembrado
    {
        public int cod_sembrado { get; set; }
        public decimal? cantidad_sembrado { get; set; }
        public string unidad_sembrado { get; set; }
        public int cod_brote { get; set; }
        public decimal? cantidad_abono { get; set; }
        public string unidad_abono { get; set; }
        public int cod_abono { get; set; }
        public decimal? cantidad_fertilizante { get; set; }
        public string unidad_fertilizante  { get; set; }
        public int cod_fertilizante { get; set; }
        public int? cod_finca { get; set; }
        public DateTime fecha_registro { get; set; }

        public string fecha_registroStr { get { return fecha_registro.ToString("dd/MM/yyyy hh:mm:ss tt"); } }

    }
}