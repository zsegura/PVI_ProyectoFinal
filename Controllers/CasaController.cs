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
        
        public ActionResult Index()
        {
            List<ModelCasa> casasList;

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                var casas = db.SpListarCasas(null).ToList();

                // Map the properties according to the exact names in SpListarCasasResult
                casasList = casas.Select(casa => new ModelCasa
                {
                    IdCasa = casa.Id_casa,
                    NombreCasa = casa.Nombre_casa,
                    MetrosCuadrados = casa.Metros_cuadrados,
                    NumeroHabitaciones = casa.Numero_habitaciones,
                    NumeroBanos = casa.Numero_banos,
                    FechaConstruccion = casa.Fecha_construccion,
                    _Estado = casa.Estado,
                    NombrePersona = casa.Propietario_nombre // Owner's full name
                }).ToList();
            }

            return View(casasList);
        }

    }
}