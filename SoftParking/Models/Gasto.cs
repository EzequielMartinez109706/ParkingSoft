using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public float Monto { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public string Periodo { get; set; }
    }
}