using DataModels;
using PVI_ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_ProyectoFinal.Controllers
{
    public class CondominioController : Controller
    {
        // GET: Casa - List all Casas based on their Estado (active/inactive)
        
        public ActionResult ConsultarCobros()
        {
            var list = new List<SpConsultarCobrosResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.SpConsultarCobros(null, null, null, null).ToList();


            }
            return View(list);
        }

    }
}