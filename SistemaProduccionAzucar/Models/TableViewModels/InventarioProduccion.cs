using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class InventarioProduccion
    {
        public int cod_producto { get; set; }
        public string descripcion { get; set; }
        public decimal? cantidad { get; set; }
        public string unidad { get; set; }
        public string tipo_producto { get; set; }
    }
}