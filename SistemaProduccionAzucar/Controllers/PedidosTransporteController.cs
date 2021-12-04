using SistemaProduccionAzucar.Models.Login;
using SistemaProduccionAzucar.Models.Pedidos;
using SistemaProduccionAzucar.Models.TableViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaProduccionAzucar.Controllers
{
    public class PedidosTransporteController : Controller
    {
        // GET: PedidosTransporte
        public ActionResult Index()
        {
            List<PedidosViewModel> list = null;

            using (PedidosEntities db = new PedidosEntities())
            {
                list = (from d in db.pedidos_canha
                        orderby d.fecha_pedido descending
                        select new PedidosViewModel
                        {
                            correlativo = d.correlativo,
                            descripcion = d.descripcion,
                            cant_pedido = d.cant_pedido,
                            estado = d.estado,
                            fechaPedido = d.fecha_pedido,
                            usuarioPedido = d.usuario_pedido,
                            prioridad = d.prioridad,
                            fechaDeseada = d.fecha_deseada,
                            fechaRecibida = d.fecha_recibida,
                            estadoStr = (
                                 d.estado == "C" ? "Cancelado" :
                                 d.estado == "A" ? "Entregado" :
                                 d.estado == "E" ? "En proceso" :
                                 "Pendiente"
                            )
                        }).ToList();
            }

            return View(list);
        }

        public ActionResult Details(int correlativo)
        {
            using (PedidosEntities db = new PedidosEntities())
            {
                PedidosViewModel ped = (from d in db.pedidos_canha
                                        orderby d.fecha_pedido descending
                                        where d.correlativo == correlativo
                                        select new PedidosViewModel
                                        {
                                            correlativo = d.correlativo,
                                            descripcion = d.descripcion,
                                            cant_pedido = d.cant_pedido,
                                            estado = d.estado,
                                            fechaPedido = d.fecha_pedido,
                                            usuarioPedido = d.usuario_pedido,
                                            prioridad = d.prioridad,
                                            fechaDeseada = d.fecha_deseada,
                                            fechaRecibida = d.fecha_recibida,
                                            estadoStr = (
                                                 d.estado == "C" ? "Cancelado" :
                                                 d.estado == "A" ? "Entregado" :
                                                 d.estado == "E" ? "En proceso" :
                                                 "Pendiente"
                                            )
                                        }).ToList().First();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = ped
                });
            }
        }

        public ActionResult getVehiculosActivos()
        {
            List<Vehiculos> list = null;

            using (PedidosEntities db = new PedidosEntities())
            {
                list = (from m in db.vehiculos_p
                        where m.estado_registro == 1 && m.disponibilidad == "Sin cargar" && m.idDUI_motorista != null
                        orderby m.idDUI_motorista ascending
                        select new Vehiculos
                        {
                            placa = m.placa,
                            motorista = m.motorista,
                            idDUI_motorista = m.idDUI_motorista,
                            disponibilidad = m.disponibilidad
                        }).ToList();
            }

            return Json(new
            {
                response = 1,
                message = "Éxito",
                data = list
            });
        }

        public ActionResult inventarioDisponible()
        {
            inventario_finca_produccion obj = null;

            using (PedidosEntities db = new PedidosEntities())
            {
                obj = db.inventario_finca_produccion.Find(1);
            }

            return Json(new
            {
                response = 1,
                message = "Éxito",
                data = obj
            });
        }

        // restar de [dbo].[inventario_finca_produccion]
        // [dbo].[vehiculos] cambiar disponibilidad
        // [dbo].[pedidos_canha] actualizar placa y estado
        public ActionResult Despachar(string placa, decimal cantidadDes, int correlativoPedidoDes)
        {
            try
            {
                using (PedidosEntities db = new PedidosEntities())
                {
                    var usuario = (usuarios) Session["UserData"];

                    pedidos_canha ped = db.pedidos_canha.Find(correlativoPedidoDes);
                    ped.placa_vehiculo = placa;
                    ped.estado = "E";

                    db.Entry(ped).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    vehiculos_p ve = db.vehiculos_p.Find(placa);
                    ve.disponibilidad = "cargado";

                    db.Entry(ve).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    inventario_finca_produccion inv = db.inventario_finca_produccion.Find(1);
                    inv.cantidad = inv.cantidad - cantidadDes;

                    db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    response = 0,
                    message = "Error: " + ex.Message
                });
            }
        }
    }
}