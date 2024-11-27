using DataModels;
using PVI_ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_ProyectoFinal.Controllers
{
    public class CondominioController : Controller
    {
        // GET: ConsultarCobros
        public ActionResult ConsultarCobros()
        {
            var list = new List<SpConsultarCobrosResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.SpConsultarCobros(null, null, null, null).ToList();
            }
            return View(list);
        }

        // GET: GestionarCobro
        public ActionResult GestionarCobro(int? id)
        {
            var cobro = new ModelCobro();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                if (id != null && id > 0)
                {
                    cobro = db.SpConsultarCobros(null, null, null, true)
                        .Where(c => c.Id_cobro == id)
                        .Select(c => new ModelCobro
                        {
                            Id = c.Id_cobro,
                            Monto = c.Monto ?? 0,    // Handle nullable types
                            Mes = c.Mes ?? 1,        // Handle nullable types
                            Anno = c.Anno ?? 2024,   // Handle nullable types
                            Estado = c.Estado,
                            Cliente = c.Cliente
                        }).FirstOrDefault();
                }

                // Load dropdown options for Casas using the stored procedure
                ViewBag.Casas = db.SpRetornaCasasActivas()
                    .Select(c => new { IdCasa = c.Id_casa, NombreCasa = c.Nombre_casa })
                    .ToList();

                // Load dropdown options for Servicios
                ViewBag.Servicios = db.Servicios
                    .Where(s => s.Estado == true)
                    .Select(s => new { s.IdServicio, s.Nombre })
                    .ToList();
            }
            return View(cobro);
        }

        [HttpPost]
        public ActionResult GestionarCobro(ModelCobro cobro)
        {
            string resultado;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    if (cobro.Id == 0)
                    {
                        db.SpCrearCobro(
                            cobro.IdCasa,
                            cobro.Mes,
                            cobro.Anno,
                            cobro.Monto,
                            string.Join(",", cobro.ServiciosSeleccionados ?? new List<int>()),
                            1 // Assuming authenticated user ID
                        );
                        resultado = "Cobro creado exitosamente.";
                    }
                    else
                    {
                        db.SpActualizarCobro(
                            cobro.Id,
                            cobro.Monto,
                            string.Join(",", cobro.ServiciosSeleccionados ?? new List<int>()),
                            1 // Assuming authenticated user ID
                        );
                        resultado = "Cobro actualizado exitosamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error al guardar el cobro: {ex.Message}";
            }

            ViewBag.Resultado = resultado;

            // Reload dropdown options for Casas using the stored procedure
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                ViewBag.Casas = db.SpRetornaCasasActivas()
                    .Select(c => new { IdCasa = c.Id_casa, NombreCasa = c.Nombre_casa })
                    .ToList();

                ViewBag.Servicios = db.Servicios
                    .Where(s => s.Estado == true)
                    .Select(s => new { s.IdServicio, s.Nombre })
                    .ToList();
            }

            return View(cobro);
        }

        // New AJAX method: GetCasas
        public JsonResult GetCasas()
        {
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                var casas = db.SpRetornaCasasActivas()
                    .Select(c => new { Id = c.Id_casa, Nombre = c.Nombre_casa })
                    .ToList();

                return Json(casas, JsonRequestBehavior.AllowGet);
            }
        }

        // New AJAX method: GetServicios
        public JsonResult GetServicios()
        {
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                var servicios = db.Servicios
                    .Where(s => s.Estado == true)
                    .Select(s => new { Id = s.IdServicio, Nombre = s.Nombre })
                    .ToList();

                return Json(servicios, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
