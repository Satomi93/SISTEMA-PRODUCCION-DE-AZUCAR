using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaProduccionAzucar.Models.Vehiculos;

namespace SistemaProduccionAzucar.Controllers
{
    public class VehiculosController : Controller
    {
        // GET: Vehiculos
        public ActionResult Index()
        {
            return View();
        }
    }
}