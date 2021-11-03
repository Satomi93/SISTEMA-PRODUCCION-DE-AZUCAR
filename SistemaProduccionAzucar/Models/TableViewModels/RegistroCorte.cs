using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class RegistroCorte
    {
        public int cod_corte { get; set; }
        public decimal? cantidad_cortada { get; set; }
        public string unidad { get; set; }
        public int cod_finca { get; set; }
        public DateTime fecha_registro { get; set; }
        public string fecha_registroStr { get { return fecha_registro.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
    }
}