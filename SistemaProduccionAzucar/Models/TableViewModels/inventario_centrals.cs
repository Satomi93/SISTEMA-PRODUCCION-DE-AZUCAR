using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class inventario_centrals
    {
        public int CodMateriaPrima { get; set; }
        public string Descripcion { get; set; }
        public decimal? Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Tipo_Materia { get; set; }
    }
}