using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Vehiculos;
using SistemaProduccionAzucar.Models.TableViewModels;

namespace SistemaProduccionAzucar.Controllers
{
    public class MotoristasController : Controller
    {
        //
        // GET: /Motoristas/
        public ActionResult Index()
        {
            List<Motoristas> list = null;

            using (VehiculosEntities db = new VehiculosEntities())
            {
                list = (from m in db.motoristas
                        orderby m.idDUI ascending
                        select new Motoristas
                        {
                            idDUI = m.idDUI,
                            motorista = m.motorista,
                            edadMot = m.edadMot,
                            genMot = m.genMot,
                            tipoLicMot = m.tipoLicMot,
                            placa = m.placa,
                            Estado = m.Estado,
                        }).ToList();
            }

            return View(list);
        }

        public ActionResult AgregarMotoristas(int idDUI, string motorista, int edad, string genero, string tipoLic)
        {
            using (VehiculosEntities db = new VehiculosEntities())
            {
                var list = from m in db.motoristas
                           where m.idDUI == idDUI
                           select m;

                if (list.Count() > 0)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "Motorista ya existe."
                    });
                }
                else
                {
                    motoristas motor = new motoristas();
                    motor.idDUI = idDUI;
                    motor.motorista = motorista;
                    motor.edadMot = edad;
                    motor.genMot = genero;
                    motor.tipoLicMot = tipoLic;
                    motor.placa = "";
                    motor.Estado = 1;

                    db.motoristas.Add(motor);
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });

                }
            }
        }

        public ActionResult CambEstado(int id_motorista)
        {
            using (VehiculosEntities db = new VehiculosEntities())
            {
                motoristas mot = db.motoristas.Find(id_motorista);
                
                if (mot.Estado == 1)
                {
                    var findVehiculos = from d in db.vehiculos
                                        where d.placa == mot.placa
                                        select d;

                    var vehiculo = findVehiculos.FirstOrDefault();

                    if (findVehiculos.Count() > 0)
                    {
                        mot.placa = "";
                        vehiculo.motorista = "";
                        db.Entry(vehiculo).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(mot).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }


                mot.Estado = (mot.Estado == 1 ? 0 : 1);
                

                db.Entry(mot).State = System.Data.Entity.EntityState.Modified;
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