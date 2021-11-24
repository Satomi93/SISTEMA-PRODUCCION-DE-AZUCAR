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

        public ActionResult AgregarVehiculo (string placa, string marca, string modelo, decimal capacidad, string estadoIngreso)
        {
            using (VehiculosEntities db = new VehiculosEntities())
            {
                var list = from d in db.vehiculos
                           where d.placa == placa
                           select d;

                if (list.Count() > 0)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "Placa ya existente."
                    });
                }
                else
                {
                    vehiculos vehiculo = new vehiculos();
                    vehiculo.placa = placa;
                    vehiculo.marca = marca;
                    vehiculo.modelo = modelo;
                    vehiculo.capacidad = capacidad;
                    vehiculo.estado_ingreso = estadoIngreso;
                    vehiculo.motorista = "";
                    vehiculo.fecha_registro = DateTime.Now;
                    vehiculo.disponibilidad = "Sin cargar";
                    vehiculo.estado_registro = 1;

                    db.vehiculos.Add(vehiculo);
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });

                }
            }
        }

        public ActionResult CambiarEstado(string cod_vehiculo)
        {
            using (VehiculosEntities db = new VehiculosEntities())
            {
                vehiculos veh = db.vehiculos.Find(cod_vehiculo);
                veh.estado_registro = (veh.estado_registro == 1 ? 0 : 1);

                db.Entry(veh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    response = 1,
                    message = "Éxito"
                });

            }
        }
    }
}