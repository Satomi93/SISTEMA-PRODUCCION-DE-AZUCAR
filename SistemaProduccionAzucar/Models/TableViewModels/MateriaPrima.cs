using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class MateriaPrima
    {
        public int codMateriaPrima { get; set; }
        public int? codFinca { get; set; }
        public string finca { get; set; }
        public string nombreMateria { get; set; }
        public decimal? cantidad { get; set; }
        public string unidad { get; set; }
        public string tipo_materia { get; set; }
    }
}