using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Motoristas;
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

            using (MotoristasEntities db = new MotoristasEntities())
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
            using (MotoristasEntities db = new MotoristasEntities())
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
            using (MotoristasEntities db = new MotoristasEntities())
            {
                motoristas mot = db.motoristas.Find(id_motorista);
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