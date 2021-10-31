using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class HistorialInventarioFinca
    {
        public int correlativo { get; set; }
        public int codMateriaPrima { get; set; }
        public decimal cantidad { get; set; }
        public int codCat { get; set; }
        public string motivo { get; set; }
        public string comentario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string fechaCreacionStr { get { return fechaCreacion.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public string usuarioCreacion { get; set; }
    }
}