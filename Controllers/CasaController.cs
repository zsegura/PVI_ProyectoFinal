using DataModels;
using PVI_ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVI_ProyectoFinal.Controllers
{
    public class CasaController : Controller
    {
        // GET: Casa - List all Casas based on their Estado (active/inactive)
        public ActionResult Index(bool? estado = true)
        {
            List<ModelCasa> casasList;

            // Initialize database context with configuration name
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                // Fetch list of casas based on their estado
                var casas = db.SpListarCasas(estado).ToList();

                // Convert each Casa to a ModelCasa
                casasList = casas.Select(casa => new ModelCasa
                {
                    IdCasa = casa.IdCasa,
                    NombreCasa = casa.NombreCasa,
                    MetrosCuadrados = casa.MetrosCuadrados,
                    NumeroHabitaciones = casa.NumeroHabitaciones,
                    NumeroBanos = casa.NumeroBanos,
                    Precio = casa.Precio,
                    IdPersona = casa.IdPersona,
                    FechaConstruccion = casa.FechaConstruccion,
                    _Estado = casa.Estado
                }).ToList();
            }

            return View(casasList);
        }


        // GET: Casa/Insertar
        public ActionResult Insertar(int? id)
        {
            var casa = new ModelCasa();

            // Fetch list of propietarios (owners) for dropdown
            using (var db = new PviProyectoFinalDB())
            {
                ViewBag.Propietarios = db.Personas
                    .Where(p => p.TipoPersona == "Propietario") // assuming this filters for owners
                    .Select(p => new { p.IdPersona, Nombre = p.Nombre + " " + p.Apellido })
                    .ToList();
            }

            if (id.HasValue)
            {
                using (var db = new PviProyectoFinalDB())
                {
                    var result = db.Casas.Find(id.Value);
                    if (result != null)
                    {
                        casa = new ModelCasa
                        {
                            IdCasa = result.IdCasa,
                            NombreCasa = result.NombreCasa,
                            MetrosCuadrados = result.MetrosCuadrados,
                            NumeroHabitaciones = result.NumeroHabitaciones,
                            NumeroBanos = result.NumeroBanos,
                            Precio = result.Precio,
                            IdPersona = result.IdPersona,
                            FechaConstruccion = result.FechaConstruccion,
                            _Estado = result.Estado
                        };
                    }
                }
            }
            return View(casa);
        }


        [HttpPost]
        public JsonResult Insertar(ModelCasa casa)
        {
            string resultado;
            try
            {
                using (var db = new PviProyectoFinalDB())
                {
                    db.SpGestionarCasa(
                        casa.IdCasa == 0 ? (int?)null : casa.IdCasa,
                        casa.NombreCasa,
                        casa.MetrosCuadrados,
                        casa.NumeroHabitaciones,
                        casa.NumeroBanos,
                        casa.IdPersona,
                        casa.FechaConstruccion,
                        casa._Estado
                    );
                    resultado = "La casa ha sido guardada correctamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error al guardar la casa: {ex.Message}";
            }
            return Json(resultado);
        }
    }
}