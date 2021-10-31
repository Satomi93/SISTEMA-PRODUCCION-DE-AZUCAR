using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.TableViewModels;
using SistemaProduccionAzucar.Models.Login;

namespace SistemaProduccionAzucar.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<User> list = null;

            using (SistemaAzucarEntities db = new SistemaAzucarEntities())
            {
                list = (from d in db.usuarios
                        orderby d.cod_usuario ascending
                        select new User {
                            codUsuario = d.cod_usuario,
                            username = d.username,
                            nombre = d.nombre,
                            apellido = d.apellido,
                            correo = d.correo,
                            clave = d.clave,
                            tipoUsuario = d.tipo_usuario,
                            estado = d.estado,
                            estadoStr = d.estado == 1 ? "Activo" : "Inactivo"
                        }).ToList();
            }

            return View(list);
        }

        public ActionResult Create(string nombres, string apellidos, string username, string correo, string clave, string tipoUsuario)
        {
            using (var db = new SistemaAzucarEntities()) 
            {
                var list = from d in db.usuarios
                           where d.username == username
                           select d;

                if (list.Count() > 0)
                {
                    return Json(new
                    {
                        response = 0,
                        message = "Nombre de usuario ya existe."
                    });
                }
                else
                {
                    usuarios user = new usuarios();
                    user.nombre = nombres;
                    user.apellido = apellidos;
                    user.username = username;
                    user.correo = correo;
                    user.clave = clave;
                    user.tipo_usuario = tipoUsuario;
                    user.estado = 1;

                    db.usuarios.Add(user);
                    db.SaveChanges();

                    return Json(new
                    {
                        response = 1,
                        message = "Éxito"
                    });
                }
            }
        }

        public ActionResult ChangeState(int codUsuario)
        {
            using (var db = new SistemaAzucarEntities())
            {
                usuarios user = db.usuarios.Find(codUsuario);
                user.estado = (user.estado == 1 ? 2 : 1);

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    response = 1,
                    message = "Éxito"
                });
            }
        }

        public ActionResult Details(int codUsuario)
        {
            using (SistemaAzucarEntities db = new SistemaAzucarEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                usuarios user = db.usuarios.Find(codUsuario);

                return Json(new
                {
                    response = 1,
                    message = "Éxito",
                    data = user
                });
            }
        }
    }
}