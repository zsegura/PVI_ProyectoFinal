using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_ProyectoFinal.Models
{
    public class ModelServicio
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; } // FK to Categoria
        public bool Estado { get; set; } // Active or Inactive
    }
}