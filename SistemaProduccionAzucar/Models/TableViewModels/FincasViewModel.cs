using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaProduccionAzucar.Models.Fincas;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class FincasViewModel
    {
        public IEnumerable<fincas> tablaFincas { get; set; }
        public IEnumerable<registro_sembrado> tabla_registro_sembrado { get; set; }
        public IEnumerable<registro_corte> tabla_registro_corte { get; set; }
    }
}