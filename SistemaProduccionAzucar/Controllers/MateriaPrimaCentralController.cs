using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.MateriaPrimaCentral;
using SistemaProduccionAzucar.Models.TableViewModels;
using SistemaProduccionAzucar.Models.Login;

namespace SistemaProduccionAzucar.Controllers
{
    public class MateriaPrimaCentralController : Controller
    {
        // GET: MateriaPrimaCentral
        public ActionResult Index()
        {
            List<inventario_centrals> list = null;
            
            using (MateriaPrimaCentralEntities db = new MateriaPrimaCentralEntities())
            {
                list = (from a in db.inventario_central
                        orderby a.cod_materia_prima ascending
                        select new inventario_centrals
                        {
                            CodMateriaPrima = a.cod_materia_prima,
                            Descripcion = a.descripcion,
                            Cantidad = a.cantidad,
                            Tipo_Materia = a.tipo_materia,
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult HistoryMateriaPrima(int codMateriaPrima)
        {
            using (MateriaPrimaCentralEntities db = new MateriaPrimaCentralEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                List<historial_inventario_centrals> list = (from x in db.historial_inventario_central
                                                            where x.cod_materia_prima == codMateriaPrima
                                                            orderby x.fecha_creacion ascending
                                                            select new historial_inventario_centrals
                                                            {
                                                                Correlativo = x.correlativo,
                                                                Cod_Materia_prima = x.cod_materia_prima,
                                                                Cantidad = x.cantidad,
                                                                Cod_cat = x.cod_cat,
                                                                Motivo = x.cat_motivo_trans_inv_central.motivo,
                                                                Comentario = x.comentario,
                                                                Fecha_creacion = x.fecha_creacion,
                                                                Usuario = x.usuario_creacion
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