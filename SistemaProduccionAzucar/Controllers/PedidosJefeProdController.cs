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
    public class PedidosJefeProdController : Controller
    {
        // GET: PedidosJefeProd
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

        public ActionResult Create(string prioridad, decimal cantidad, DateTime fechaDeseada)
        {
            try
            {
                using (PedidosEntities db = new PedidosEntities())
                {
                    var usuario = (usuarios)Session["UserData"];

                    pedidos_canha ped = new pedidos_canha();
                    ped.descripcion = "Caña arrimada";
                    ped.estado = "E";
                    ped.fecha_pedido = DateTime.Now;
                    ped.usuario_pedido = usuario.username;
                    ped.prioridad = prioridad;
                    ped.cant_pedido = cantidad;
                    ped.fecha_deseada = fechaDeseada;

                    db.pedidos_canha.Add(ped);
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

        public ActionResult ChangeState(int correlativoPedido, string estadoCS)
        {
            using (PedidosEntities db = new PedidosEntities())
            {
                try
                {
                    var usuario = (usuarios)Session["UserData"];

                    pedidos_canha ped = db.pedidos_canha.Find(correlativoPedido);
                    ped.estado = estadoCS;

                    if (estadoCS == "A")
                    {
                        ped.fecha_recibida = DateTime.Now;

                        inventario_central_p inv = db.inventario_central_p.Find(1);
                        inv.cantidad = inv.cantidad + ped.cant_pedido;
                        db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        historial_inventario_central_p invc = new historial_inventario_central_p();
                        invc.cod_materia_prima = 1;
                        invc.cantidad = ped.cant_pedido;
                        invc.cod_cat = 1;
                        invc.comentario = "Por pedido de caña arrimada.";
                        invc.fecha_creacion = DateTime.Now;
                        invc.usuario_creacion = usuario.username;
                        db.historial_inventario_central_p.Add(invc);
                        db.SaveChanges();
                    }

                    db.Entry(ped).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });
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
}