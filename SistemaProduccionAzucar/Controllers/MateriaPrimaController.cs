using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.MateriaPrima;
using SistemaProduccionAzucar.Models.TableViewModels;
using SistemaProduccionAzucar.Models.Login;

namespace SistemaProduccionAzucar.Controllers
{
    public class MateriaPrimaController : Controller
    {
        // GET: MateriaPrima
        public ActionResult Index()
        {
            List<MateriaPrima> list = null;

            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                list = (from d in db.inventario_finca
                        orderby d.cod_materia_prima ascending
                        select new MateriaPrima
                        {
                            codMateriaPrima = d.cod_materia_prima,
                            codFinca = d.cod_finca,
                            finca = d.fincas.nombre,
                            nombreMateria = d.nombre,
                            cantidad = d.cantidad,
                            unidad = d.unidad,
                        }).ToList();
            }

            return View(list);
        }

        public ActionResult History(int codMateriaPrima)
        {
            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                List<HistorialInventarioFinca> list = (from m in db.historial_inventario_finca
                                                   where m.cod_materia_prima == codMateriaPrima
                                                   orderby m.fecha_creacion ascending
                                                   select new HistorialInventarioFinca
                                                   {
                                                       correlativo = m.correlativo,
                                                       codMateriaPrima = m.cod_materia_prima,
                                                       cantidad = m.cantidad,
                                                       codCat = m.cod_cat,
                                                       motivo = m.cat_motivo_trans_inv_finca.motivo,
                                                       comentario = m.comentario,
                                                       fechaCreacion = m.fecha_creacion,
                                                       usuarioCreacion = m.usuario_creacion
                                                   }).ToList();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = list
                });
            }
        }

        public ActionResult ListaFincas()
        {
            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                List<Finca> list = (from f in db.fincas
                                    orderby f.cod_finca descending
                                    select new Finca 
                                    {
                                        codFinca = f.cod_finca,
                                        nombre = f.nombre,
                                        direccion = f.direccion,
                                        areaMetrosCuadrados = f.area_metros_cuadrados
                                    }).ToList();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = list
                });
            }
        }

        public ActionResult ListaMotivos()
        {
            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                List<Motivos> list = (from f in db.cat_motivo_trans_inv_finca
                                      where f.cod_cat == 3 || f.cod_cat == 4
                                      orderby f.cod_cat ascending
                                      select new Motivos
                                      {
                                          codCat = f.cod_cat,
                                          motivo = f.motivo
                                      }).ToList();

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = list
                });
            }
        }

        public ActionResult Create(int finca, string nombre, decimal cantidad, string unidad, string tipo)
        {
            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                try
                {
                    var usuario = (usuarios)Session["UserData"];

                    inventario_finca inv = new inventario_finca();
                    inv.cod_finca = finca;
                    inv.nombre = nombre;
                    inv.cantidad = cantidad;
                    inv.unidad = unidad;
                    inv.tipo_materia = tipo;
                    db.inventario_finca.Add(inv);
                    db.SaveChanges();

                    historial_inventario_finca his = new historial_inventario_finca();
                    his.cod_materia_prima = inv.cod_materia_prima;
                    his.cantidad = cantidad;
                    his.cod_cat = 1;
                    his.comentario = "Inventario inicial";
                    his.fecha_creacion = DateTime.Now;
                    his.usuario_creacion = usuario.username;
                    db.historial_inventario_finca.Add(his);
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

        public ActionResult CreateHistory(int codMateriaPrima, int hMotivo, string hComentario, decimal hCantidad)
        {
            using (MateriaPrimaEntities db = new MateriaPrimaEntities())
            {
                try
                {
                    var usuario = (usuarios) Session["UserData"];

                    historial_inventario_finca his = new historial_inventario_finca();
                    his.cod_materia_prima = codMateriaPrima;
                    his.cantidad = hCantidad;
                    his.cod_cat = hMotivo;
                    his.comentario = hComentario;
                    his.fecha_creacion = DateTime.Now;
                    his.usuario_creacion = usuario.username;
                    db.historial_inventario_finca.Add(his);
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