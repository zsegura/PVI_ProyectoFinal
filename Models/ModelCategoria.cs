using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelCategoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; } // Active or Inactive
    }
}