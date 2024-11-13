using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelCasa
    {
        public int IdCasa { get; set; }  // This can be kept as an identifier, even if not displayed in the view
        public string NombreCasa { get; set; }
        public int MetrosCuadrados { get; set; }
        public int NumeroBanos { get; set; }
        public int NumeroHabitaciones { get; set; }
        public DateTime FechaConstruccion { get; set; }
        public bool _Estado { get; set; }
        public string NombrePersona { get; set; }  // Owner's name
    }


}