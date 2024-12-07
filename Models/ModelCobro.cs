using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelCobro
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Anno { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string Cliente { get; set; }
        public string Casa { get; set; } // Holds Nombre_casa
        public int IdCasa { get; set; } // Add this property
        public List<int> ServiciosSeleccionados { get; set; }
    }



}