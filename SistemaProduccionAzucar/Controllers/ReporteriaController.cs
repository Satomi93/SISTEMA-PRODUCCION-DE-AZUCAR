using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using SistemaProduccionAzucar.Models.InventarioProduccion;
using SistemaProduccionAzucar.Models.TableViewModels;

namespace SistemaProduccionAzucar.Controllers
{
    public class ReporteriaController : Controller
    {
        // GET: Reporteria
        public ActionResult Index()
        {
            List<InventarioProduccion> list = null;

            using (InventarioProduccionEntities db = new InventarioProduccionEntities())
            {
                list = (from p in db.inventario_produccion
                        orderby p.cod_producto ascending
                        select new InventarioProduccion
                        {
                            cod_producto = p.cod_producto,
                            descripcion = p.descripcion
                        }).ToList();

                return View(list);
            }
        }



        public ActionResult generarReporte(int codPro)
        {
            InventarioProduccionEntities db = new InventarioProduccionEntities();

            List<RegistroProduccion> list = (from m in db.registro_produccion
                                             where m.cod_producto == codPro
                                             orderby m.fecha_registro ascending
                                             select new RegistroProduccion
                                             {
                                                 correlativo = m.correlativo,
                                                 materia_prima = m.materia_prima,
                                                 cantidad = m.cantidad,
                                                 unidad_m = m.unidad_m,
                                                 estado = m.estado,
                                                 producto = m.producto,
                                                 cantidad_producida = m.cantidad_producida,
                                                 unidad_p = m.unidad_p,
                                                 fecha_registro = m.fecha_registro,
                                                 fecha_fin = m.fecha_fin
                                             }).ToList();

            return new ViewAsPdf("generarReporte", list) { };
        }
    }
}