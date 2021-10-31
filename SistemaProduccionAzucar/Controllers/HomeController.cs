﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Login;

namespace SistemaProduccionAzucar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            try
            {
                using (SistemaAzucarEntities db = new SistemaAzucarEntities())
                {
                    var list = from d in db.usuarios 
                               where d.username == username
                               select d;

                    if (list.Count() > 0)
                    {
                        usuarios user = list.First();

                        if (user.clave == password)
                        {
                            if (user.estado == 1) 
                            {
                                Session["UserData"] = user;
                                string url = "";

                                switch(user.tipo_usuario) {
                                    case "Administrador":
                                        url = "/User/";
                                        break;
                                    case "Administrador finca":
                                        url = "/MateriaPrima/";
                                        break;
                                    case "Encargado transporte caña":
                                        url = "~/User/";
                                        break;
                                    case "Jefe de área":
                                        url = "~/User/";
                                        break;
                                    case "Encargado de central":
                                        url = "~/User/";
                                        break;
                                }

                                return Json(new
                                {
                                    response = 1,
                                    message = "Éxito",
                                    url = url
                                });
                            }
                            else
                            {
                                return Json(new
                                {
                                    response = 2,
                                    message = "Usuario inactivo."
                                });
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                response = 2,
                                message = "Clave incorrecta."
                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            response = 2,
                            message = "Usuario no encontrado."
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    response = 0,
                    message = "Error: " + e.Message
                });
            }
        }

        public ActionResult Logout()
        {
            Session["UserData"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}