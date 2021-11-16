using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Vehiculos;
using SistemaProduccionAzucar.Models.TableViewModels;

namespace SistemaProduccionAzucar.Controllers
{
    public class VehiculosController : Controller
    {
        // GET: Vehiculos
        public ActionResult Index()
        {
            List <Vehiculos> list = null;

            using(VehiculosEntities db = new VehiculosEntities())
            {
                list = (from d in db.vehiculos
                        orderby d.placa ascending
                        select new Vehiculos
                        {
                            placa = d.placa,
                            marca = d.marca,
                            modelo = d.modelo,
                            capacidad = d.capacidad,
                            motorista = d.motorista,
                            estado_ingreso = d.estado_ingreso,
                            fecha_ingreso = d.fecha_registro,
                            disponibilidad = d.disponibilidad,
                            estado_registro = d.estado_registro,
                        }).ToList();
            }

                return View(list);
        }
    }
}