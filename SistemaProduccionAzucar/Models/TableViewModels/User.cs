using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class User
    {
        public int codUsuario { get; set; }
        public string username { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string tipoUsuario { get; set; }
        public string estadoStr { get; set; }
        public int estado { get; set; }
    }
}