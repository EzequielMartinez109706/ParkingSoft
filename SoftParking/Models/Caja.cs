using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Caja
    {
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public long Anio { get; set; }
    }
}