using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaProduccionAzucar.Models.TableViewModels
{
    public class PedidosViewModel
    {

        public int correlativo { get; set; }
        public string descripcion { get; set; }
        public decimal cant_pedido { get; set; }
        public string estado { get; set; }
        public DateTime? fechaPedido { get; set; }
        public string usuarioPedido { get; set; }
        public string prioridad { get; set; }
        public DateTime? fechaDeseada { get; set; }
        public DateTime? fechaRecibida { get; set; }
        public string placaVehiculo { get; set; }

        public string estadoStr { get; set; }
        public string fechaPedidoStr { get { return (fechaPedido == null ? null : fechaPedido.Value.ToString("dd/MM/yyyy hh:mm:ss tt")); } }
        public string fechaDeseadaStr { get { return (fechaDeseada == null ? null : fechaDeseada.Value.ToString("dd/MM/yyyy hh:mm:ss tt")); } }
        public string fechaRecibidaStr { get { return (fechaRecibida == null ? null : fechaRecibida.Value.ToString("dd/MM/yyyy hh:mm:ss tt")); } }
    }
}