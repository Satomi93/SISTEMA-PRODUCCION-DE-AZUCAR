using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class Finca
    {
        public int codFinca { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public decimal? areaMetrosCuadrados { get; set; }
    }
}