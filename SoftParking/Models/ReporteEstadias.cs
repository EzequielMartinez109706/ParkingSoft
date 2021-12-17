using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class ReporteEstadias
    {
        public string Dominio { get; set; }
        public string TipoEstadia { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEgreso { get; set; }
    }
}