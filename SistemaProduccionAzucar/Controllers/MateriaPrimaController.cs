using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.MateriaPrima;
using SistemaProduccionAzucar.Models.TableViewModels;

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

    }
}