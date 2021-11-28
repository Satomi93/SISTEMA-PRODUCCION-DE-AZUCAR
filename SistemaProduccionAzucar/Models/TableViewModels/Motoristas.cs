using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class Motoristas
    {
        public int idDUI { get; set; }
        public string motorista { get; set; }
        public int edadMot { get; set; }
        public string genMot { get; set; }
        public string tipoLicMot { get; set; }
        public string placa { get; set; }
        public int Estado { get; set; }
    }
}