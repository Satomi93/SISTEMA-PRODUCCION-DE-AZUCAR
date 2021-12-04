using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class historial_inventario_centrals
    {
        public int Correlativo { get; set; }
        public int Cod_Materia_prima { get; set; }
        public decimal? Cantidad { get; set; }
        public int Cod_cat { get; set; }
        public string Motivo { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public string fechaCreacionStr { get { return Fecha_creacion.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public string Usuario { get; set; }
    }
}